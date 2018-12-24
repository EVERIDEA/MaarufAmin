using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourMoveEvent : GameEvent
{
    public Vector2 Position;

    public PlayerBehaviourMoveEvent(Vector2 position)
    {
        Position = position;
    }
}
public class PlayerBehaviourChangeSpeedEvent : GameEvent
{
    public float Speed;
    public bool IsRestart;

    public PlayerBehaviourChangeSpeedEvent(float speed, bool isRestart)
    {
        Speed = speed;
        IsRestart = isRestart;
    }
}
