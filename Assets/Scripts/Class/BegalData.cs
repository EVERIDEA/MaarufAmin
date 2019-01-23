using UnityEngine;

[System.Serializable]
public class BegalData {

    public Vector3 Rotation;
    public Vector3 Jump;
    public float MovementSpeed;
    public float PanicMovementSpeed;
    public int PanicMaxTime;
    public int Direction;
    public Vector2 PlayerPos;
    public float Distance;
    public bool IsOnCatch;
    public float RotationSpeed;
    public float JumpPower;
    public float JumpDuration;
    public float CurMoveSpeed;

    public int CheckMethodRun;
    public int IndexBehaviour;
    public float TempPanicMovement;

    public int PanicTime;
    public bool IsRob;

    public BegalData(Vector3 rotation, Vector3 jump, float movementSpeed, float panicMovementSpeed, 
        int panicMaxTime, int direction, Vector2 playerPos, float distance, bool isOnCatch, float rotationSpeed, 
        float jumpPower, float jumpDuration, float curMoveSpeed, int checkMethodRun, int indexBehaviour, 
        float tempPanicMovement, int panicTime, bool rob)
    {
        Rotation = rotation;
        Jump = jump;
        MovementSpeed = movementSpeed;
        PanicMovementSpeed = panicMovementSpeed;
        PanicMaxTime = panicMaxTime;
        Direction = direction;
        PlayerPos = playerPos;
        Distance = distance;
        IsOnCatch = isOnCatch;
        RotationSpeed = rotationSpeed;
        JumpPower = jumpPower;
        JumpDuration = jumpDuration;
        CurMoveSpeed = curMoveSpeed;
        CheckMethodRun = checkMethodRun;
        IndexBehaviour = indexBehaviour;
        TempPanicMovement = tempPanicMovement;
        PanicTime = panicTime;
        IsRob = rob;
    }

    public static BegalData CopyData (BegalData data) {
        BegalData newData = new BegalData(
        data.Rotation,
        data.Jump,
        data.MovementSpeed,
        data.PanicMovementSpeed,
        data.PanicMaxTime,
        data.Direction,
        data.PlayerPos,
        data.Distance,
        data.IsOnCatch,
        data.RotationSpeed,
        data.JumpPower,
        data.JumpDuration,
        data.CurMoveSpeed,
        data.CheckMethodRun,
        data.IndexBehaviour,
        data.TempPanicMovement,
        data.PanicTime,
        data.IsRob);

        return newData;
    }
}
