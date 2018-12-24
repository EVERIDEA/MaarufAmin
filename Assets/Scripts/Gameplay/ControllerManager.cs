using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ControllerManager : MonoBehaviour {
    
    Tween _Tween;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            Debug.Log(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);


            mousePos2D = SetMinMaxPlayerPosition(mousePos2D);
            EventManager.TriggerEvent(new PlayerBehaviourMoveEvent(mousePos2D));

            if(hit.collider != null)
            {
                Debug.Log("Something was clicked! = "+hit.transform.tag);
                //hit.collider.attachedRigidbody.AddForce(Vector2.up);
            }
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
