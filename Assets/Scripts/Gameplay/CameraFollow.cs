using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public static CameraFollow thisClass;

    public Transform target;
    public Vector3 offset;
    public float limitLeft;
    public float limitRight;
    public float limitTop;
    public float limitBottom;
    public float followSpeed;

    GameObject player;
    Camera camera;
	// Use this for initialization
	void Start () {
        thisClass = this;
        player = target.gameObject;
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (!player.activeInHierarchy)
        {
            var desirePos = new Vector3(0, 2f, -10f);
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desirePos, followSpeed);
            transform.position = smoothPosition;
        }
        else
        {
            Vector3 desirePosition = target.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desirePosition, followSpeed);
            transform.position = new Vector3(
            Mathf.Clamp(smoothPosition.x, limitLeft, limitRight),
            Mathf.Clamp(smoothPosition.y, limitTop, limitBottom),
            smoothPosition.z);
        }

        if (GameController.Instance.DataGameplay.IsGameReady)
        {
            if (camera.orthographicSize > 7.5)
            {
                camera.orthographicSize -= Time.deltaTime * 2f;
            }
        }
    }
}
