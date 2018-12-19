using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour, IButtonInit {
    
    [SerializeField]
    GameObject _PlayButton;
    [SerializeField]
    GameObject _HelpButton;
    [SerializeField]
    GameObject _ExitButton;

    // Use this for initialization
    void Awake()
    {
        InitOnAwake();
    }

    public void InitOnAwake()
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
        EventManager.TriggerEvent(new MainMenuButtonEvent(EMainMenuButton.HELP, true));
        //write function button here
    }
    void OnCallExitButton()
    {
        EventManager.TriggerEvent(new MainMenuButtonEvent(EMainMenuButton.EXIT));
        //write function button here
    }
}
