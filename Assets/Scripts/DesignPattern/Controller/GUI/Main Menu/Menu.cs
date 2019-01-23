using System;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Menu : GameStateMachine<Menu>
{

    public enum States{
        None,
        Story,
        Playing,
        Options,
        Credit,
        Share
    }
    

    private GameObject[] Buttons;

    private void Awake()
    {
        Initialize<States>();
        Buttons = GameObject.FindGameObjectsWithTag("UIButton");
    }

    private void Start()
    {
        SignalManager.Instance.AttachReceiver("button.menuui", this.OnSignalReceived);
    }

    private void OnSignalReceived(Dictionary<string, object> eventParam)
    {
        var action = (String) eventParam["action"];
        Debug.Log("Signal Received");
        switch (action) {
            case "menu.play":
                ChangeState(States.Story);
                break;
            case "menu.option":
                ChangeState(States.Options);
                break;
            case "menu.credit":
                ChangeState(States.Credit);
                break;
            case "menu.share":
                Debug.Log("METHOD DOESN'T EXIST");
                break;
            case "menu.quit":
                Application.Quit();
                Debug.Log("QUIT");
                break;
            case "menu.close":
                ChangeToPreviousState();
                break;
            case "story.play":
                Application.LoadLevel("Game");
                break;
            //default:
            //    ChangeToPreviousState();
            //    break;
        }
    }

    public void ToggleButtons(bool Show)
    {
        Buttons.ToList().ForEach(g => g.gameObject.SetActive(Show));
    }
}
