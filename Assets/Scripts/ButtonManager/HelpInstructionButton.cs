using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpInstructionButton : MonoBehaviour, IButtonInit {

    [SerializeField]
    GameObject _ExitInstructuionButton;

    void Awake()
    {
        InitOnAwake();
    }

    public void InitOnAwake()
    {
        _ExitInstructuionButton.AddComponent<Button>().onClick.AddListener(delegate {
            OnCallExitInstructuionButton();
        });
    }
    void OnCallExitInstructuionButton()
    {
        EventManager.TriggerEvent(new MainMenuButtonEvent(EMainMenuButton.HELP, false));
    }
}
