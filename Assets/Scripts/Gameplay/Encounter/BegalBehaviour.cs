using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BegalBehaviour : EncounterBehaviour
{
    Tween tween;
    private BegalData dataBegal;
    PlayerData playerData;
    public float curMoveSpeed;
    public float curPanicMoveSpeed;
    public int Direction;
    //state initialize
    private void Awake()
    {
        EventManager.AddListener<InitializeDataEvent<BegalData>>(InitializeDataListener);
    }
    private void InitializeDataListener(InitializeDataEvent<BegalData> e)
    {
       // Debug.Log($@"On Initialize : {e.Data.JumpPower}");
        playerData = GameController.Instance.GetGameplayData<PlayerData>("player.data");

        dataBegal = BegalData.CopyData(e.Data);

        curMoveSpeed = dataBegal.Direction;
        curPanicMoveSpeed = dataBegal.PanicMovementSpeed;
        InitEncounterBehaviour(EEncounterBehaviourType.WALKING);
    }
    //procedure when encounter were killed
    protected override void OnDead()
    {
        dataBegal.Jump = new Vector2(dataBegal.Jump.x * playerData.direction, dataBegal.Jump.y);
        tween = transform.DORotate(dataBegal.Rotation, dataBegal.RotationSpeed, RotateMode.Fast);
        tween = transform.DOJump(dataBegal.Jump, dataBegal.JumpPower, 1, dataBegal.JumpDuration, false);
    }
    //procedure when encounter were idle
    protected override void OnIdle()
    {
        RobbingHandler();
    }
    //procedure when encounter were running
    protected override void OnRun()
    {
        switch (dataBegal.Direction)
        {
            case -1:
                curPanicMoveSpeed = dataBegal.PanicMovementSpeed * 1;
                break;
            case 1:
                curPanicMoveSpeed = dataBegal.PanicMovementSpeed * -1;
                break;
        }
        curPanicMoveSpeed = dataBegal.PanicMovementSpeed;
        if(dataBegal.IsOnCatch)
            dataBegal.PanicTime += 1;
    }
    //procedure when encounter were walking
    protected override void OnWalking()
    {
        curMoveSpeed = dataBegal.MovementSpeed;
    }
    //procedure when encounter were facing to the direction
    void CheckFlip(float dir)
    {
        if (!dataBegal.IsOnCatch)
        {
            if (dir < 0)
                Direction = -1;
            else
                Direction = 1;
            transform.localScale = new Vector2(Direction, transform.localScale.y);
        }
    }

    void WalkBehaviour()
    {
        if (!dataBegal.IsOnCatch)
        {
            CheckFlip(curMoveSpeed);
            transform.position = new Vector2(
                        transform.position.x + Time.deltaTime * curMoveSpeed,
                        transform.position.y);
        }
    }

    void CatchHandler()
    {
        if (dataBegal.IsOnCatch)
        {
            if (Player.Instance.DataPlayer.direction == -1)
            {
                transform.position = new Vector2(
                    Player.Instance.transform.position.x - 2f,
                    Player.Instance.transform.position.y);
            }
            else
            {
                transform.position = new Vector2(
                    Player.Instance.transform.position.x + 2f,
                    Player.Instance.transform.position.y);

            }

            if (dataBegal.IsRob)
            {
                //Add Some More Reputations
            }
        }
    }

    void RobbingHandler()
    {
        if (dataBegal.IsRob)
        {
            InitEncounterBehaviour(EEncounterBehaviourType.RUN);
        }
    }

    private void FixedUpdate()
    {
        WalkBehaviour();
        CatchHandler();

        if (dataBegal.PanicTime > (dataBegal.PanicMaxTime - 1))
        {
            if(dataBegal.Direction == 1)
            {
                dataBegal.Direction = -1;
            }
            else
            {
                dataBegal.Direction = 1;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                if (!Player.Instance.DataPlayer.isOnGrass)
                {
                    if (!Player.Instance.DataPlayer.isOnCathingPeople)
                    {
                        dataBegal.IsOnCatch = true;
                        InitEncounterBehaviour(EEncounterBehaviourType.DEAD);
                    }
                }
                break;
            case "PanicTrigger":
                if (!Player.Instance.DataPlayer.isOnGrass)
                {
                    if (dataBegal.PanicTime < dataBegal.PanicMaxTime)
                    {
                        InitEncounterBehaviour(EEncounterBehaviourType.RUN);
                    }
                }
                break;
            case "People":
                if (!collision.GetComponent<PeopleBehaviour>().isRobbed)
                {
                    dataBegal.IsRob = true;
                    collision.GetComponent<PeopleBehaviour>().isRobbed = true;
                }
                InitEncounterBehaviour(EEncounterBehaviourType.IDLE);
                break;
            case "PeopleTrigger":
                curMoveSpeed = dataBegal.CurMoveSpeed * 3;
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Player.Instance.DataPlayer.isOnGrass)
        {
            if (collision.tag.Equals("PanicTrigger"))
            {
                if (dataBegal.CheckMethodRun == 0)
                {
                    if (dataBegal.PanicTime < dataBegal.PanicMaxTime)
                    {
                        InitEncounterBehaviour(EEncounterBehaviourType.RUN);
                    }
                    dataBegal.CheckMethodRun = dataBegal.CheckMethodRun + 1;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Player.Instance.DataPlayer.isOnGrass)
        {
            if (collision.tag.Equals("PanicTrigger"))
            {
                dataBegal.IsOnCatch = false;
                if (dataBegal.PanicTime < dataBegal.PanicMaxTime)
                {
                    StartCoroutine(BackToNormal(1));
                }
            }
        }
    }
    
    IEnumerator BackToNormal(float time)
    {
        yield return new WaitForSeconds(time);
        curMoveSpeed = dataBegal.MovementSpeed;
        curPanicMoveSpeed = dataBegal.TempPanicMovement;
        dataBegal.CheckMethodRun = 0;
    }
}
