using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract class witch represents the components associated with a base behaviour class<T>
/// </summary>
public abstract class StateComponentBase<T> : MonoBehaviour where T : GameStateMachine<T>
{
    protected T Behaviour;

    [HideInInspector]
    public Enum NextState;

    [HideInInspector]
    public bool IsActive;
    
    public virtual void Awake()
    {
        Behaviour = GetComponent<T>();
    }
    public abstract void EnterState();
    
    public abstract void ExitState();

}