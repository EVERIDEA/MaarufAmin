using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public static PlayerBehaviour thisClass;

    [HideInInspector]
    Transform _PlayerTransform;
    public float Speed = 2;
    public bool isOnCathingPeople = false;

    Tween _Tween;
    float _DefaultSpeed;

    // Use this for initializatio
    private void Awake()
    {
        thisClass = this;
        _PlayerTransform = thisClass.transform;
        _DefaultSpeed = Speed;

        EventManager.AddListener<PlayerBehaviourMoveEvent>(MovePlayerListener);
        EventManager.AddListener<PlayerBehaviourChangeSpeedEvent>(ChangeSpeedListener);
    }

    private void ChangeSpeedListener(PlayerBehaviourChangeSpeedEvent e)
    {
        if(e.IsRestart)
            Speed = _DefaultSpeed;
        else
            Speed = e.Speed;
    }

    private void MovePlayerListener(PlayerBehaviourMoveEvent e)
    {
        if(_Tween != null)
            _Tween.Kill();
        _Tween = _PlayerTransform.DOMove(e.Position, Speed);
    }

}
