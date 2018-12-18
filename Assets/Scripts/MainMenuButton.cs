using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : ButtonManager {
    
    [SerializeField]
    GameObject _PlayButton;
    [SerializeField]
    GameObject _HelpButton;
    [SerializeField]
    GameObject _ExitButton;

    // Use this for initialization
    protected override void InitOnAwake()
    {
        _PlayButton.AddComponent<Button>().onClick.AddListener(delegate {
            OnCallPlayButton();
        });
        _HelpButton.AddComponent<Button>().onClick.AddListener(delegate {
            OnCallHelpButton();
        });
        _ExitButton.AddComponent<Button>().onClick.AddListener(delegate {
            OnCallExitButton();
        });
    }

    void OnCallPlayButton()
    {
        //write function button here
        EventManager.TriggerEvent(new MainMenuButtonEvent(EMainMenuButton.PLAY));
    }
    void OnCallHelpButton()
    {
        //write function button here
        Debug.Log("HELP BUTTON");
    }
    void OnCallExitButton()
    {
        //write function button here
        Debug.Log("EXIT BUTTON");
    }
}
