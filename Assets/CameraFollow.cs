using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    public float limitLeft;
    public float limitRight;
    public float limitTop;
    public float limitBottom;
    public float followSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 desirePosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position,desirePosition,followSpeed);
        //transform.position = smoothPosition;
        transform.position = new Vector3(
            Mathf.Clamp(smoothPosition.x, limitLeft, limitRight), 
            Mathf.Clamp(smoothPosition.y, limitTop, limitBottom), 
            smoothPosition.z);
    }
}
