using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonEvent : GameEvent
{
    public EMainMenuButton Type;
    public bool IsActive;

    public MainMenuButtonEvent(EMainMenuButton type, bool isActive = true)
    {
        Type = type;
        IsActive = isActive;        
    }
}
