using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BegalBehaviour : EncounterBehaviour
{
    Tween _Tween;
    public Vector3 _Rotation;
    public Vector3 _Jump;
    public float _MovementSpeed;
    public float _RotationSpeed;
    public float _JumpPower;
    public float _JumpDuration;
    public int _Direction;

    private void Start()
    {
        InitEncounterBehaviour(EEncounterBehaviourType.WALKING);
    }

    protected override void OnDead()
    {
        Debug.Log("DEAD");
        if (_Tween != null)
            _Tween.Kill();

        _Tween.Play<Tween>();
        _Tween = this.transform.DORotate(_Rotation, _RotationSpeed, RotateMode.Fast);
        _Tween = this.transform.DOJump(_Jump, _JumpPower, 1, _JumpDuration, false);
    }

    protected override void OnIdle()
    {
        _Tween.Pause<Tween>();
    }

    protected override void OnRun()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnWalking()
    {
        if (_Tween != null)
            _Tween.Kill();

        if (!CheckDirection(_Direction))
            this.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        else
            this.transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

        _Tween = this.transform.DOMove(new Vector2(_Direction, transform.position.y), _MovementSpeed, false);
        Debug.Log(CheckDirection(_Direction));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (!PlayerBehaviour.thisClass.isOnGrass)
                InitEncounterBehaviour(EEncounterBehaviourType.DEAD); 
        }
    }
}
