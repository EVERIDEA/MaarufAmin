using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonEvent : GameEvent
{
    public EMainMenuButton Type;

    public MainMenuButtonEvent(EMainMenuButton type)
    {
        Type = type;
    }
}
