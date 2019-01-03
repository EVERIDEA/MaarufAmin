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
    public bool isStayOnGrass = true;
    public bool isFacingRight = true;
    public SpriteRenderer spriteRenderer;
    public int direction = 0; 

    Tween _Tween;
    float _DefaultSpeed;
    
    float changeTime = 0;

    private void Awake()
    {
        thisClass = this;
    }

    // Use this for initializatio
    private void Start()
    {
        _PlayerTransform = thisClass.transform;
        _DefaultSpeed = Speed;
    }

    private void Update()
    {
        if (isOnGrass)
        {
            spriteRenderer.sortingOrder = -3;
            transform.position = Vector3.zero;
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
        if (changeTime >= 1 && changeTime < 2)
        {
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        }
        else if (changeTime >= 2)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            changeTime = 0;
        }
    }

}
