using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SysObj = System.Object;

[Serializable]
public class GameStateDataBase : ScriptableObject
{
	public String Key;
	public SysObj DefaultValue;

	public virtual void Initialize () { }

}