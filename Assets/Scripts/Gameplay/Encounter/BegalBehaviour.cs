using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BegalBehaviour : EncounterBehaviour
{
    protected override void OnDead()
    {
        Debug.Log("DEAD");
    }

    protected override void OnIdle()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnRun()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnWalking()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            InitEncounterBehaviour(EEncounterBehaviourType.DEAD);
        }
    }
}
