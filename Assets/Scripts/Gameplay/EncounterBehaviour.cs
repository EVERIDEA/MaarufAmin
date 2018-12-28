using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EncounterBehaviour : MonoBehaviour {
    public EEncounterType Type;
    // Use this for initialization

    public void InitEncounterBehaviour(EEncounterBehaviourType TYPE)
    {
        OnBehaviourListener(TYPE);
    }

    void OnBehaviourListener(EEncounterBehaviourType type)
    {
        switch(type)
        {
            case EEncounterBehaviourType.WALKING:
                OnWalking();
                break;
            case EEncounterBehaviourType.IDLE:
                StartCoroutine(TimeBeforeDead(1));
                break;
            case EEncounterBehaviourType.RUN:
                OnRun();
                break;
            case EEncounterBehaviourType.DEAD:
                OnDead();
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

    IEnumerator TimeBeforeDead(float time)
    {
        OnIdle();
        PlayerBehaviour.thisClass.isOnCathingPeople = true;
        yield return new WaitForSeconds(time);
        OnDead();
        PlayerBehaviour.thisClass.isOnCathingPeople = false;
    }
}
