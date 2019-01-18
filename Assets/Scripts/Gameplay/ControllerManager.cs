using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ControllerManager : MonoBehaviour {
    
    public GameObject player;

    Transform playerTransform;
    //PlayerBehaviour playerBehaviour;

    float checkDirection = 0;
    int direction = 0;

    Vector3 mousePos;
    Vector2 mousePos2D;

    bool isOnTargetPos = false;
    bool isOnPlaying = false;

    bool isTempOnGrass;

    private void Awake()
    {
        playerTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    

}
