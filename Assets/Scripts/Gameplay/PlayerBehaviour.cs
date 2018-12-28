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
    public bool isOnGrass = true;
    public bool isFacingRight = true;

    Tween _Tween;
    float _DefaultSpeed;
    SpriteRenderer spriteRenderer;
    float changeTime = 0;

    // Use this for initializatio
    private void Start()
    {
        thisClass = this;
        _PlayerTransform = thisClass.transform;
        _DefaultSpeed = Speed;
        spriteRenderer = GetComponent<SpriteRenderer>();

        EventManager.AddListener<PlayerBehaviourMoveEvent>(MovePlayerListener);
        EventManager.AddListener<PlayerBehaviourChangeSpeedEvent>(ChangeSpeedListener);
    }

    private void Update()
    {
        if (isOnGrass)
        {
            spriteRenderer.sortingOrder = -3;
            OnIdle();
        }
        else
        {
            spriteRenderer.sortingOrder = 1;
        }
    }

    private void OnIdle()
    {
        changeTime += Time.deltaTime;
        if (changeTime >= 2 && changeTime < 4)
        {
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        }
        else if (changeTime >= 4)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            changeTime = 0;
        }
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
