using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTriggerManager : MonoBehaviour {

    public static GrassTriggerManager thisClass;

    public bool isClickOnGrass = false;

    private void Awake()
    {
        thisClass = this;
    }

    private void OnMouseDown()
    {
        isClickOnGrass = true;
        Debug.Log("OnGrass");
    }
}
