using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeDataEvent <T> : GameEvent
{
    public T Data;

    public InitializeDataEvent (T data)
    {
        Data = data;
    }
}
