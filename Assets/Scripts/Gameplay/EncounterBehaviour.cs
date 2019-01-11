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
                OnIdle();
                break;
            case EEncounterBehaviourType.RUN:
                OnRun();
                break;
            case EEncounterBehaviourType.DEAD:
                StartCoroutine(TimeBeforeDead(1));
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
        yield return new WaitForSeconds(time);
        OnDead();
        PlayerBehaviour.thisClass.isOnCathingPeople = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
