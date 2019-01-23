using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameplayData {
    public int DayCount;
    public bool IsGameReady;
    public bool IsGameReset;
    public float CurrentTime;
    public float CurrentPlus;
    public float Time;
    public bool BreakingUp;
    public GameObject [] Encounters;

    public GameplayData(int dayCount, bool isGameReady, bool isGameReset, float currentTime, float currentPlus, float time, bool breakingUp, GameObject[] encounters)
    {
        DayCount = dayCount;
        IsGameReady = isGameReady;
        IsGameReset = isGameReset;
        CurrentTime = currentTime;
        CurrentPlus = currentPlus;
        Time = time;
        BreakingUp = breakingUp;
        Encounters = encounters;
    }
}