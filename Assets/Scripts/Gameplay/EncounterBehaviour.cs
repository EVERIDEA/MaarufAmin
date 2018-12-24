using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EncounterBehaviour : MonoBehaviour {
    public EEncounterType Type;
    // Use this for initialization

    public void InitEncounterBehaviour()
    {

    }

    void OnBehaviourListener(EEncounterBehaviourType type)
    {
        switch(type)
        {
            case EEncounterBehaviourType.WALKING:
                OnWalking();
                break;
            case EEncounterBehaviourType.IDLE:
                OnIdle();
                break;
            case EEncounterBehaviourType.RUN:
                break;
            case EEncounterBehaviourType.DEAD:
                break;
            case EEncounterBehaviourType.TAKE_PICTURE:
                if (Type != EEncounterType.PERS)
                    return;
                break;
            case EEncounterBehaviourType.ATTACK:
                if(Type != EEncounterType.BEGAL)
                    return;
                break;
        }
    }

    protected abstract void OnWalking();
    protected abstract void OnIdle();
    protected abstract void OnRun();
    protected abstract void OnDead();

    virtual protected void OnAttack()
    {
    }
    virtual protected void OnTakePicture()
    {
    }
}
