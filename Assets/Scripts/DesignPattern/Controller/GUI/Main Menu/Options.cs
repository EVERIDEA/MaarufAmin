using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Options : StateComponentBase<Menu>
{
    [SerializeField]
    protected GameObject PanelUI;
    //[SerializeField]
    //private Slider musicSlider;

    //[SerializeField]
    //private Slider soundFXSlider;

    protected override void OnStart()
    {
        base.OnStart();
    }

    public override void EnterState()
    {
        Behaviour.ToggleButtons(false);
        PanelUI.SetActive(true);
        //PlayerPrefs.GetFloat("Music", musicSlider.value);
        //PlayerPrefs.GetFloat("SoundFX", soundFXSlider.value);
    }

    public override void ExitState()
    {
        Behaviour.ToggleButtons(true);
        //PlayerPrefs.SetFloat("Music", musicSlider.value);
        //PlayerPrefs.SetFloat("SoundFX", soundFXSlider.value);
        PanelUI.SetActive(false);
    }

    protected override void OnUpdateFrame(float deltaTime)
    {
        base.OnUpdateFrame(deltaTime);
    }

    //public void GoBack()
    //{
    //    Behaviour.ChangeToPreviousState();
    //}
}
