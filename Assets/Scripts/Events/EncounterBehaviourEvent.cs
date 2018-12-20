using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterBehaviourActionEvent : GameEvent
{
    public EEncounterBehaviourType Type;

    public EncounterBehaviourActionEvent(EEncounterBehaviourType type)
    {
        Type = type;
    }
}
