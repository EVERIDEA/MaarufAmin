using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using SysObj = System.Object;
using UniObj = UnityEngine.Object;

public class RepositoryManager : Singleton<RepositoryManager>
{
    private Dictionary<String, SysObj> states;
    private Dictionary<String, SysObj> statesUpdate;

    public delegate void OnGameStatesResetDelegate();
    public delegate void OnGameStatesUpdateDelegate(Dictionary<String, SysObj> states);

    private event OnGameStatesResetDelegate onGameStatesReset;
    private event OnGameStatesUpdateDelegate onGameStatesUpdate;

    public event OnGameStatesResetDelegate OnGameStatesReset
    {
        add
        {
            lock (Sync)
            {
                this.onGameStatesReset -= value;
                this.onGameStatesReset += value;
            }
        }
        remove
        {
            lock (Sync)
            {
                this.onGameStatesReset -= value;
            }
        }
    }

    public event OnGameStatesUpdateDelegate OnGameStatesUpdate
    {
        add
        {
            lock (Sync)
            {
                this.onGameStatesUpdate -= value;
                this.onGameStatesUpdate += value;
            }
        }
        remove
        {
            lock (Sync)
            {
                this.onGameStatesUpdate -= value;
            }
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        this.states = new Dictionary<String, SysObj>();
        this.statesUpdate = new Dictionary<String, SysObj>();
        //Physics2D.Raycast()
        var states = Resources.LoadAll<GameStateDataBase>($@"{Constant.ResourcePathData}Game States");

        for (var c = 0; c < states.Length; c++)
        {
            states[c].Initialize();
            this.states.Add(states[c].Key, states[c].DefaultValue);
        }

    }

    private void InvokeOnGameStatesReset() => this.onGameStatesReset?.Invoke();

    private void InvokeOnGameStatesUpdate()
    {
        this.onGameStatesUpdate?.Invoke(this.statesUpdate);
        this.statesUpdate.Clear();
    }

    public T GetState<T>(String key)
    {
        return (T)(!this.states.ContainsKey(key) ? default(T) : this.states[key]);
    }

    public SysObj GetState(String key) => !this.states.ContainsKey(key) ? null : this.states[key];

    public Dictionary<String, SysObj> GetStates() => this.states;

    public void ResetStates()
    {
        var states = Resources.LoadAll<GameStateDataBase>($@"{Constant.ResourcePathData}Game States");

        for (var c = 0; c < states.Length; c++)
        {
            this.states[states[c].Key] = states[c].DefaultValue;
        }

        this.InvokeOnGameStatesReset();
    }

    public void SetState<T>(String key, T value, Boolean update = true)
    {
        if (!this.states.ContainsKey(key))
        {
            this.states.Add(key, value);
        }
        else
        {
            this.states[key] = value;
        }

        this.statesUpdate.Add(key, value);

        if (update)
        {
            this.InvokeOnGameStatesUpdate();
        }
    }

    public void SetState(String key, SysObj value) => this.SetState<SysObj>(key, value);

    public void SetStates(Dictionary<String, SysObj> states)
    {
        foreach (var entry in states)
        {
            this.SetState(entry.Key, entry.Value, false);
        }

        this.InvokeOnGameStatesUpdate();
    }
}