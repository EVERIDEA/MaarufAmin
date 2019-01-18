using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersBehaviour : EncounterBehaviour
{
    public float _Speed;
    public int _Direction;
    public int _CatchDistance;

    float curMoveSpeed;

    public bool isOnFollow = false;

    Transform target;
    Vector3 curScale;

    private void Start()
    {
        InitPeopleBehaviour();
    }

    void InitPeopleBehaviour()
    {
        _Speed = _Speed * _Direction;
        //target = PlayerBehaviour.thisClass.transform;
        curScale = GetComponent<Transform>().localScale;
        InitEncounterBehaviour(EEncounterBehaviourType.WALKING);
    }

    protected override void OnDead()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnIdle()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnRun()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnWalking()
    {
        curMoveSpeed = _Speed;
    }

    void NewsHandler(Transform kiGuruTransform,float maxDistance)
    {
        float distance = Vector2.Distance(kiGuruTransform.position,transform.position);
        if (distance >= 0 && distance < maxDistance)
        { //In a Distance            
            if (!Player.Instance.DataPlayer.isOnGrass)
            {
                // On Follow ki Guru
                isOnFollow = true;
            }
            else
            {
                isOnFollow = false;
            }
        }
        else
        {
            if (Player.Instance.DataPlayer.isOnGrass)
            {
                isOnFollow = false;
            }
        }
    }

    Vector3 ChecLookPos()
    {
        Vector3 newPos;
        if (Player.Instance.DataPlayer.direction == 1)
        {
            return newPos = new Vector3(-6, 0, 0);
        }
        else
        {
            return newPos = new Vector3(6, 0, 0);
        }
    }

    void WalkingBehaviour()
    {
        curMoveSpeed = _Speed;
        if (isOnFollow)
        {
            Vector3 onFollowPos = target.position + ChecLookPos();
            Vector3 smoothPosition = Vector3.Lerp(transform.position, onFollowPos, 0.05f);
            transform.position = smoothPosition;

            if (transform.position.x < target.position.x)
                transform.localScale = new Vector3(1,1,1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = curScale;
            transform.position = new Vector2(
               transform.position.x + Time.deltaTime * curMoveSpeed,
               transform.position.y);
        }
       
    }

    private void LateUpdate()
    {
        NewsHandler(target, _CatchDistance);
        WalkingBehaviour();
    }
}
