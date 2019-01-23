using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameStateDataGeneric<T> : GameStateDataBase
{
	public new T DefaultValue;

	public override void Initialize () => base.DefaultValue = this.DefaultValue;
}