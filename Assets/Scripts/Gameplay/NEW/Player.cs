using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : GameStateMachine<Player> {

    public enum States
    {
        None,
        Move,
        Attack,
        Hide,
        Dead
    }

    private static Player _instance;

    public static Player Instance
    {
        get

        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<Player>();

            if (_instance == null)
            {
                GameObject singleton = new GameObject("Player");
                _instance = singleton.AddComponent<Player>();
            }

            //DontDestroyOnLoad(_instance);
            return _instance;
        }
    }
    private SpriteRenderer spriteRenderer;

    private PlayerData dataPlayer;
    public PlayerData DataPlayer
    {
        get
        {
            if (dataPlayer == null)
                dataPlayer = GameController.Instance.GetGameplayData<PlayerData>("player.data");

            return dataPlayer;
        }
    }

    protected override void OnAwake()
    {
        Initialize<States>();
        if (spriteRenderer == null)
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        EventManager.AddListener<InitializeDataEvent<PlayerData>>(InitializeDataListener);
    }

    private void InitializeDataListener(InitializeDataEvent<PlayerData> e)
    {
        dataPlayer = e.Data;
    }

    // Use this for initializatio
    protected override void OnStart()
    {
        SignalManager.Instance.AttachReceiver("player.behaviour", this.OnSignalReceived);
        
        ChangeState(States.Move);
        GameController.Instance.DataGameplay.IsGameReady = true;
    }

    protected override void OnUpdateFrame(float deltaTime)
    {
        if (dataPlayer.isOnGrass)
        {
            spriteRenderer.sortingOrder = -3;
            base.transform.position = Vector3.zero;
            OnIdle();
        }
        else
        {
            spriteRenderer.sortingOrder = 1;
        }
    }

    private void OnIdle()
    {
        dataPlayer.changeTime += Time.deltaTime;
        if (dataPlayer.changeTime >= 1 && dataPlayer.changeTime < 2)
        {
            base.transform.localScale = new Vector2(base.transform.localScale.x, base.transform.localScale.y);
        }
        else if (dataPlayer.changeTime >= 2)
        {
            base.transform.localScale = new Vector2(-base.transform.localScale.x, base.transform.localScale.y);
            dataPlayer.changeTime = 0;
        }
    }

    public Transform GetTransform() {
        return transform;
    }

    private void OnSignalReceived(Dictionary<string, object> eventParam)
    {
        var action = (System.String)eventParam["action"];
        Debug.Log("Signal Received");
        switch (action)
        {
            case "something.action":

                break;
            default:
                ChangeToPreviousState();
                break;
        }
    }
}
