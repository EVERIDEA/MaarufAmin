using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SysObj = System.Object;

public class SignalDispatcher : MonoBehaviour {

    private Button button;
    public System.String Identifier = "button.menuui:action=name,type=type,value=val";
    //state.alter:action=set,type=int32,value=-10
    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
        if (button == null)
            button = gameObject.AddComponent<Button>();
        
    }
    private void Start()
    {
        this.button.onClick.AddListener(() =>
        {
            Emitter();
        });
    }

    void Emitter() {

        if (String.IsNullOrEmpty(Identifier) || String.IsNullOrWhiteSpace(Identifier))
        {
            return;
        }

        var data = Identifier.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
        var compiled = new Dictionary<String, SysObj>();

        if (data.Length > 1)
        {
            var parameters = data[1].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            for (var c = 0; c < parameters.Length; c++)
            {
                var param = parameters[c].Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                var key = param[0];
                // TODO : Update / Create function to automatically parse the data type
                var val = param[1];
                compiled.Add(key, val);
            }
        }

        SignalManager.Instance.DispatchSignal(data[0], compiled);
    }
}
