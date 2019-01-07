using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleBehaviour : EncounterBehaviour
{

    public float _Speed;
    public int _Direction;

    public bool isRobbed = false;
    float curMoveSpeed;
    
    SpriteRenderer render;
    Color color;

    private void Start()
    {
        InitPeopleBehaviour();
    }

    void InitPeopleBehaviour()
    {
        _Speed = _Speed * _Direction;
        render = GetComponent<SpriteRenderer>();
        color = render.color;
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

    void FadingOut()
    {
        color.a -= Time.deltaTime;
        render.color = color;
    }

    void WalkingBehaviour()
    {
        if (!isRobbed)
        {
            transform.position = new Vector2(
                transform.position.x + Time.deltaTime * curMoveSpeed,
                transform.position.y);
        }
        else
        {
            FadingOut();
            if (color.a < 0.01)
                Destroy(this.gameObject); //People Disapear
        }
    }

    private void FixedUpdate()
    {
        WalkingBehaviour();
    }
}
