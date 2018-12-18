using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour {

    [SerializeField]
    GameObject MainMenuHUD;
    [SerializeField]
    GameObject StoryHUD;
    // Use this for initialization
    void Start ()
    {
        EventManager.AddListener<MainMenuButtonEvent>(MainMenuButtonListener);
	}

    void MainMenuButtonListener(MainMenuButtonEvent evt)
    {
        switch(evt.Type)
        {
            case EMainMenuButton.PLAY:
                Debug.Log("PLAY BUTTON");
                break;
            case EMainMenuButton.HELP:
                break;
            case EMainMenuButton.EXIT:
                break;
        }
    }
}
