using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : StateComponentBase<Player>
{
    [SerializeField]
    float speed = 2;

    float defaultSpeed;

    public GameObject player;

    Transform playerTransform;

    float checkDirection = 0;
    int direction = 0;

    Vector3 mousePos;
    Vector2 mousePos2D;

    bool isOnTargetPos = false;
    bool isOnPlaying = false;
    
    public override void EnterState()
    {
        defaultSpeed = speed;
        playerTransform = player.transform;
    }

    public override void ExitState()
    {


    }

    void Update()
    {
        if (GameController.Instance.DataGameplay.IsGameReady)
        {
            if (Input.GetMouseButtonDown(0) && !isOnPlaying)
            {
                Player.Instance.DataPlayer.isOnGrass = false;
                isOnPlaying = true;
            }
            if (!Player.Instance.DataPlayer.isOnCathingPeople)
            {
                if (isOnPlaying)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        mousePos2D = new Vector2(mousePos.x, mousePos.y);

                        FlipPlayer(mousePos);

                        playerTransform.localScale = new Vector2(direction, playerTransform.localScale.y);

                        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                        mousePos2D = SetMinMaxPlayerPosition(mousePos2D);
                    }
                }
            }
            else
            {
                //Player is Bussy
            }
            playerTransform.position = Vector2.MoveTowards(playerTransform.position, mousePos2D, speed * Time.deltaTime);
        }

        if (GrassTriggerManager.thisClass.isClickOnGrass)
        {
            if (playerTransform.position.x == mousePos2D.x)
            {
                if (GrassTriggerManager.thisClass.isReadyToGrass)
                {
                    //PlayerBehaviour.thisClass.isOnGrass = true;
                    isOnPlaying = false;
                    GrassTriggerManager.thisClass.isClickOnGrass = false;
                }
            }
        }
    }

    void FlipPlayer(Vector3 pos)
    {
        Vector3 lastPos = playerTransform.position;
        checkDirection = (pos - lastPos).normalized.x;

        if (checkDirection < 0.01)
            direction = -1;
        else if (checkDirection > 0.01)
            direction = 1;

        Player.Instance.DataPlayer.direction = direction;
    }

    Vector2 SetMinMaxPlayerPosition(Vector2 mousePos)
    {
        Vector2 playerPosition = mousePos;
        if (playerPosition.y >= 1.3f)
            playerPosition = new Vector2(playerPosition.x, 1f);
        if (playerPosition.y <= -3f)
            playerPosition = new Vector2(playerPosition.x, -3f);

        return playerPosition;
    }
}
