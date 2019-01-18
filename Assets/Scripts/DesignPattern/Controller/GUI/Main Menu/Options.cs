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

    private void Start()
    {
        //musicSlider.value = AudioManager.Instance.MusicEnvironment.volume;
        //soundFXSlider.value = AudioManager.Instance.SoundFX.volume;

        //musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        //soundFXSlider.onValueChanged.AddListener(AudioManager.Instance.SetSoundFXVolume);
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

    private void Update()
    {
        //if(IsActive)
        //    optionPanel.localPosition = Vector3.Lerp(optionPanel.localPosition, new Vector3(0,0,0), slideSpeed * Time.deltaTime);
        //else
        //    optionPanel.localPosition = Vector3.Lerp(optionPanel.localPosition, new Vector3(0,-800,0), slideSpeed * Time.deltaTime);
    }

    public void GoBack()
    {
        Behaviour.ChangeToPreviousState();
    }
}
