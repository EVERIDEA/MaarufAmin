using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Share : StateComponentBase<Menu>
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
