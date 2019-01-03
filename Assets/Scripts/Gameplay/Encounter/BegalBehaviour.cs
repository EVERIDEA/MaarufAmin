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
    public float _RotationSpeed;
    public float _JumpPower;
    public float _JumpDuration;
    public int _Direction;

    Vector2 playerPos;
    float distance;
    bool isOnCatch = false;

    [SerializeField]
    float curMoveSpeed;

    int checkMethodRun = 0;
    int indexBehaviour = 0;

    //state initialize
    private void Start()
    {
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
        _Tween.Kill();
    }

    //procedure when encounter were running
    protected override void OnRun()
    {
        CheckFlip(_Direction);
        curMoveSpeed = _PanicMovementSpeed;
    }

    //procedure when encounter were walking
    protected override void OnWalking()
    {
        CheckFlip(_Direction);
        curMoveSpeed = _MovementSpeed;
    }

    //procedure when encounter were facing to the direction
    void CheckFlip(int dir)
    {
        if (!isOnCatch)
        {
            if (dir > 0)
                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
            else
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    //procedure to check State of walk
    void WalkBehaviour()
    {
        if (!isOnCatch)
        {
            transform.transform.position = new Vector2(
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
        }
    }

    private void Update()
    {
        WalkBehaviour();
        CatchHandler();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (!PlayerBehaviour.thisClass.isOnGrass)
            {
                isOnCatch = true;
                InitEncounterBehaviour(EEncounterBehaviourType.DEAD);
            }
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
                    _PanicMovementSpeed = _PanicMovementSpeed * -1;
                    InitEncounterBehaviour(EEncounterBehaviourType.RUN);
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
                StartCoroutine(BackToNormal(2));
            }
        }
    }

    //time to check if encounter not panic anymore
    IEnumerator BackToNormal(float time)
    {
        yield return new WaitForSeconds(time);
        curMoveSpeed = _MovementSpeed;
    }
}
