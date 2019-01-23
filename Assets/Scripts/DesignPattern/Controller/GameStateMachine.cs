using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GameStateMachine<T> : BaseBehaviour where T : GameStateMachine<T>
{
    private Dictionary<Enum, StateComponentBase<T>> MachineStates;
    protected Enum PreviousStateID;
    protected Enum CurrentStateID;

    [SerializeField]
    protected StateComponentBase<T> CurrentState;
    
    private bool AllowDebug = false;
    
    protected void Initialize<E>() 
    {
        if(!typeof(E).IsEnum)
            throw new ArgumentException("Generic reference must be an enumerated type");

        Array values = Enum.GetValues(typeof(E));

        MachineStates = new Dictionary<Enum, StateComponentBase<T>>();

        //Iterate with enums values
        for(int i = 0; i < values.Length; i++){

            Enum e = (Enum)values.GetValue(i);

            Component Comp = GetComponent(values.GetValue(i).ToString());

            MachineStates.Add(e, (StateComponentBase<T>)Comp);
        }

        if(MachineStates.Count > 0) {
            CurrentState    = MachineStates.First().Value;
            CurrentStateID  = MachineStates.First().Key;
            PreviousStateID = CurrentStateID; 

            EnterState();
        }

        DebugInfo();

    }

    public Enum GetCurrentState()
    {
        return CurrentStateID;
    }

    public void ChangeToPreviousState() => ChangeState(PreviousStateID);

    public void ChangeState(Enum ToState)
    {
        if(CurrentStateID.Equals(ToState))
            return;

        ExitState(ToState);

        if(MachineStates.ContainsKey(ToState)){
            PreviousStateID = CurrentStateID;
            CurrentState = MachineStates[ToState];
            CurrentStateID = ToState;
        }
        else
            Debug.LogWarning("Enum key was not founded");

        EnterState();
    }

    private void EnterState()
    {
        if(CurrentState != null){
            CurrentState.IsActive = true;
            CurrentState.EnterState();
        }
    }

    private void ExitState(Enum NextState)
    {
        if(CurrentState != null) {
            CurrentState.NextState = NextState;
            CurrentState.ExitState();
            CurrentState.IsActive = false;
        }
    }

    private void DebugInfo()
    {
        if(AllowDebug)
            foreach(KeyValuePair<Enum, StateComponentBase<T>> obj in MachineStates)
                Debug.Log(obj);
    }

}
