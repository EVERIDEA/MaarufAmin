using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BegalBehaviour : EncounterBehaviour
{
    Tween _Tween;
    public Vector3 _Rotation;
    public Vector3 _Jump;
    public float _MovementSpeed;
    public float _PanicMovementSpeed;
    public int _PanicMaxTime;
    public int _Direction;

    Vector2 playerPos;
    float distance;
    bool isOnCatch = false;
    float _RotationSpeed = 3f;
    float _JumpPower = 8;
    float _JumpDuration = 2;

    [SerializeField]
    float curMoveSpeed;

    int checkMethodRun = 0;
    int indexBehaviour = 0;
    float tempPanicMovement;

    int panicTime = 0;

    [SerializeField]
    bool isRob = false;

    //state initialize
    private void Start()
    {
        _MovementSpeed = _Direction;
        tempPanicMovement = _PanicMovementSpeed;
        curMoveSpeed = _MovementSpeed;
        InitEncounterBehaviour(EEncounterBehaviourType.WALKING);
    }

    //procedure when encounter were killed
    protected override void OnDead()
    {
        _Jump = new Vector2(_Jump.x * PlayerBehaviour.thisClass.direction, _Jump.y);
        _Tween = transform.DORotate(_Rotation, _RotationSpeed, RotateMode.Fast);
        _Tween = transform.DOJump(_Jump, _JumpPower, 1, _JumpDuration, false);
    }

    //procedure when encounter were idle
    protected override void OnIdle()
    {
        RobbingHandler();
    }

    //procedure when encounter were running
    protected override void OnRun()
    {
        switch (_Direction)
        {
            case -1:
                _PanicMovementSpeed = _PanicMovementSpeed * 1;
                break;
            case 1:
                _PanicMovementSpeed = _PanicMovementSpeed * -1;
                break;
        }
        curMoveSpeed = _PanicMovementSpeed;
        if(isOnCatch)
            panicTime += 1;
    }

    //procedure when encounter were walking
    protected override void OnWalking()
    {
        curMoveSpeed = _MovementSpeed;
    }

    //procedure when encounter were facing to the direction
    void CheckFlip(float dir)
    {
        if (!isOnCatch)
        {
            if (dir < 0)
                _Direction = -1;
            else
                _Direction = 1;
            transform.localScale = new Vector2(_Direction, transform.localScale.y);
        }
    }

    //procedure to check State of walk
    void WalkBehaviour()
    {
        if (!isOnCatch)
        {
            CheckFlip(curMoveSpeed);
            transform.position = new Vector2(
                        transform.position.x + Time.deltaTime * curMoveSpeed,
                        transform.position.y);
        }
    }

    //procedure to check State of catching
    void CatchHandler()
    {
        if (isOnCatch)
        {
            if (PlayerBehaviour.thisClass.direction == -1)
            {
                transform.position = new Vector2(
                    PlayerBehaviour.thisClass.transform.position.x - 2f,
                    PlayerBehaviour.thisClass.transform.position.y);
            }
            else
            {
                transform.position = new Vector2(
                     PlayerBehaviour.thisClass.transform.position.x + 2f,
                     PlayerBehaviour.thisClass.transform.position.y);

            }

            if (isRob)
            {
                //Add Some More Reputations
            }
        }
    }

    void RobbingHandler()
    {
        if (isRob)
        {
            InitEncounterBehaviour(EEncounterBehaviourType.RUN);
        }
    }

    private void FixedUpdate()
    {
        WalkBehaviour();
        CatchHandler();

        if (panicTime > (_PanicMaxTime - 1))
        {
            if(_Direction == 1)
            {
                _Direction = -1;
            }
            else
            {
                _Direction = 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (!PlayerBehaviour.thisClass.isOnGrass)
            {
                if (!PlayerBehaviour.thisClass.isOnCathingPeople)
                {
                    isOnCatch = true;
                    InitEncounterBehaviour(EEncounterBehaviourType.DEAD);
                }
            }
        }
        if (!PlayerBehaviour.thisClass.isOnGrass)
        {
            if (collision.tag.Equals("PanicTrigger"))
            {
                if (panicTime < _PanicMaxTime)
                {
                    InitEncounterBehaviour(EEncounterBehaviourType.RUN);
                }
            }
        }

        if (collision.tag.Equals("People"))
        {
            if (!collision.GetComponent<PeopleBehaviour>().isRobbed)
            {
                isRob = true;
                collision.GetComponent<PeopleBehaviour>().isRobbed = true;
            }
            InitEncounterBehaviour(EEncounterBehaviourType.IDLE);
        }

        if (collision.tag.Equals("PeopleTrigger"))
        {
            curMoveSpeed = curMoveSpeed * 3;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!PlayerBehaviour.thisClass.isOnGrass)
        {
            if (collision.tag.Equals("PanicTrigger"))
            {
                if(checkMethodRun == 0)
                {
                    if (panicTime < _PanicMaxTime)
                    {
                        InitEncounterBehaviour(EEncounterBehaviourType.RUN);
                    }
                    checkMethodRun = checkMethodRun + 1;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!PlayerBehaviour.thisClass.isOnGrass)
        {
            if (collision.tag.Equals("PanicTrigger"))
            {
                isOnCatch = false;
                if (panicTime < _PanicMaxTime)
                {
                    StartCoroutine(BackToNormal(1));
                }
            }
        }
    }

    //time to check if encounter not panic anymore
    IEnumerator BackToNormal(float time)
    {
        yield return new WaitForSeconds(time);
        curMoveSpeed = _MovementSpeed;
        _PanicMovementSpeed = tempPanicMovement;
        checkMethodRun = 0;
    }
}
