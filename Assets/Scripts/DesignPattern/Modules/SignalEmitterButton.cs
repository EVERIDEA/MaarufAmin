using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using SysObj = System.Object;

public class SignalEmitterButton : SignalEmitter
{
	private Button button;

	public String SignalIdentifier;

	protected override void OnAwake ()
	{
		base.OnAwake ();

		this.button = this.GetComponent<Button> ();
	}

	protected override void OnStart ()
	{
		base.OnStart ();

		this.button.onClick.AddListener (() =>
		 {
			 this.Emit (this.SignalIdentifier);
		 });
	}
}