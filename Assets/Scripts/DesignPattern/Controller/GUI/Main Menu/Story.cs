using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : StateComponentBase<Menu>
{
    [SerializeField]
    protected GameObject PanelUI;
    public override void EnterState()
    {
        Behaviour.ToggleButtons(false);
        PanelUI.SetActive(true);
    }

    public override void ExitState()
    {
        Behaviour.ToggleButtons(true);
        PanelUI.SetActive(false);
    }

    public void GoBack()
    {
        Behaviour.ChangeToPreviousState();
    }
}
