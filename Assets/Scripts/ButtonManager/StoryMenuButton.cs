using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryMenuButton : MonoBehaviour, IButtonInit
{

    [SerializeField]
    GameObject _PlayButton;
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
            EventManager.TriggerEvent(new MainMenuButtonEvent(EMainMenuButton.START_GAME));
        });
        _ExitButton.AddComponent<Button>().onClick.AddListener(delegate {
            EventManager.TriggerEvent(new MainMenuButtonEvent(EMainMenuButton.START_GAME, false));
        });
    }
}
