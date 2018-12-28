using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ControllerManager : MonoBehaviour {
    
    public GameObject player;

    Transform playerTransform;
    PlayerBehaviour playerBehaviour;

    float checkDirection = 0;
    int direction = 0;

    private void Start()
    {
        playerTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update () {
        if (!PlayerBehaviour.thisClass.isOnCathingPeople)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlayerBehaviour.thisClass.isOnGrass = false;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                Debug.Log(mousePos2D);

                var lastPos = playerTransform.position;
                checkDirection = (mousePos - lastPos).normalized.x;
                if (checkDirection < 0.01)
                    direction = -1;
                else if (checkDirection > 0.01)
                    direction = 1;

                playerTransform.localScale = new Vector2(direction, playerTransform.localScale.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                mousePos2D = SetMinMaxPlayerPosition(mousePos2D);

                EventManager.TriggerEvent(new PlayerBehaviourMoveEvent(mousePos2D));
            }
        }
        else
        {
            Debug.Log("KI ARUF AMIN IS BUSSY");
        }
    }

    Vector2 SetMinMaxPlayerPosition(Vector2 mousePos)
    {
        Vector2 playerPosition = mousePos;
        if(playerPosition.y >= 1.3f)
            playerPosition = new Vector2(playerPosition.x, 1.2f);
        if(playerPosition.y <= -2.5f)
            playerPosition = new Vector2(playerPosition.x, -2.5f);

        return playerPosition;
    }
}
