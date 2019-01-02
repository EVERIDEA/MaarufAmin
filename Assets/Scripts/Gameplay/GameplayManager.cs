using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    public static GameplayManager thisClass;

    int _DayCount;

    [SerializeField]
    public bool isGameReady = false;

    // Use this for initialization
    private void Awake()
    {
        thisClass = this;
    }

    public void InitGameplay ()
    {

    } 
}
