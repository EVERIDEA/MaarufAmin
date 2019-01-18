using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool isOnCathingPeople;
    public bool isOnGrass;
    public bool isStayOnGrass;
    public bool isFacingRight;
    public int direction;
    public float changeTime;

    public PlayerData(bool isOnCathingPeople, bool isOnGrass, bool isStayOnGrass, bool isFacingRight, int direction, float changeTime)
    {
        this.isOnCathingPeople = isOnCathingPeople;
        this.isOnGrass = isOnGrass;
        this.isStayOnGrass = isStayOnGrass;
        this.isFacingRight = isFacingRight;
        this.direction = direction;
        this.changeTime = changeTime;
    }
}

