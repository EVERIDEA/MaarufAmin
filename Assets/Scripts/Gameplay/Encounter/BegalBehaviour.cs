using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BegalBehaviour : EncounterBehaviour
{
    Tween tween;
    private BegalData dataBegal;

    PlayerData playerData;
    //state initialize

    private void Awake()
    {
        EventManager.AddListener<InitializeDataEvent<BegalData>>(InitializeDataListener);
    }

    private void InitializeDataListener(InitializeDataEvent<BegalData> e)
    {
        Debug.Log($@"On Initialize : {e.Data.JumpPower}");
        playerData = GameController.Instance.GetGameplayData<PlayerData>("player.data");

        dataBegal = BegalData.CopyData(e.Data);

        dataBegal.MovementSpeed = dataBegal.Direction;
        dataBegal.TempPanicMovement = dataBegal.PanicMovementSpeed;
        dataBegal.CurMoveSpeed = dataBegal.MovementSpeed;

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
                dataBegal.PanicMovementSpeed = dataBegal.PanicMovementSpeed * 1;
                break;
            case 1:
                dataBegal.PanicMovementSpeed = dataBegal.PanicMovementSpeed * -1;
                break;
        }
        dataBegal.CurMoveSpeed = dataBegal.PanicMovementSpeed;
        if(dataBegal.IsOnCatch)
            dataBegal.PanicTime += 1;
    }

    //procedure when encounter were walking
    protected override void OnWalking()
    {
        dataBegal.CurMoveSpeed = dataBegal.MovementSpeed;
    }

    //procedure when encounter were facing to the direction
    void CheckFlip(float dir)
    {
        if (!dataBegal.IsOnCatch)
        {
            if (dir < 0)
                dataBegal.Direction = -1;
            else
                dataBegal.Direction = 1;
            transform.localScale = new Vector2(dataBegal.Direction, transform.localScale.y);
        }
    }

    //procedure to check State of walk
    void WalkBehaviour()
    {
        if (!dataBegal.IsOnCatch)
        {
            CheckFlip(dataBegal.CurMoveSpeed);
            transform.position = new Vector2(
                        transform.position.x + Time.deltaTime * dataBegal.CurMoveSpeed,
                        transform.position.y);
        }
    }

    //procedure to check State of catching
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
        if (collision.tag.Equals("Player"))
        {
            if (!Player.Instance.DataPlayer.isOnGrass)
            {
                if (!Player.Instance.DataPlayer.isOnCathingPeople)
                {
                    //isOnCatch = true;
                    InitEncounterBehaviour(EEncounterBehaviourType.DEAD);
                }
            }
        }
        if (!Player.Instance.DataPlayer.isOnGrass)
        {
            if (collision.tag.Equals("PanicTrigger"))
            {
                if (dataBegal.PanicTime < dataBegal.PanicMaxTime)
                {
                    InitEncounterBehaviour(EEncounterBehaviourType.RUN);
                }
            }
        }

        if (collision.tag.Equals("People"))
        {
            if (!collision.GetComponent<PeopleBehaviour>().isRobbed)
            {
                dataBegal.IsRob = true;
                collision.GetComponent<PeopleBehaviour>().isRobbed = true;
            }
            InitEncounterBehaviour(EEncounterBehaviourType.IDLE);
        }

        if (collision.tag.Equals("PeopleTrigger"))
        {
            dataBegal.CurMoveSpeed = dataBegal.CurMoveSpeed * 3;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Player.Instance.DataPlayer.isOnGrass)
        {
            if (collision.tag.Equals("PanicTrigger"))
            {
                //if (checkMethodRun == 0)
                //{
                //    if (panicTime < _PanicMaxTime)
                //    {
                //        InitEncounterBehaviour(EEncounterBehaviourType.RUN);
                //    }
                //    checkMethodRun = checkMethodRun + 1;
                //}
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Player.Instance.DataPlayer.isOnGrass)
        {
            if (collision.tag.Equals("PanicTrigger"))
            {
                //isOnCatch = false;
                //if (panicTime < _PanicMaxTime)
                //{
                //    StartCoroutine(BackToNormal(1));
                //}
            }
        }
    }

    //time to check if encounter not panic anymore
    IEnumerator BackToNormal(float time)
    {
        yield return new WaitForSeconds(time);
        dataBegal.CurMoveSpeed = dataBegal.MovementSpeed;
        dataBegal.PanicMovementSpeed = dataBegal.TempPanicMovement;
        dataBegal.CheckMethodRun = 0;
    }
}
