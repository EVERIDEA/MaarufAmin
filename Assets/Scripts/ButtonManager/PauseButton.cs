using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour, IButtonInit
{

    [SerializeField]
    GameObject _ExitPauseButton;

    void Awake()
    {
        InitOnAwake();
    }

    public void InitOnAwake()
    {
        _ExitPauseButton.AddComponent<Button>().onClick.AddListener(delegate {
            OnCallPauseButton();
        });
    }
    void OnCallPauseButton()
    {
        EventManager.TriggerEvent(new MainMenuButtonEvent(EMainMenuButton.PAUSE, false));
    }
}
