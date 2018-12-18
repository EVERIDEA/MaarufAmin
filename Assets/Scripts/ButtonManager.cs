﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonManager : MonoBehaviour
{
    private void Awake()
    {
        InitOnAwake();
    }

    virtual protected void InitOnAwake() { }

}
