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

public class SignalManager : Singleton<SignalManager>
{
	private Dictionary<String , Signal> signals;

	protected override void OnAwake()
	{
		base.OnAwake ();

		this.signals = new Dictionary<String , Signal> ();
	}

	public void AttachReceiver(String key , BasicEventHandler<Dictionary<String , SysObj>> val)
	{
		lock (Sync)
		{
			if (!this.signals.ContainsKey (key))
			{
				this.AttachSignal (key);
			}

			this.signals [key].Receiver += val;
		}
	}

	public void AttachSignal(String key)
	{
		lock (Sync)
		{
			if (this.signals.ContainsKey (key))
			{
				return;
			}

			this.signals.Add (key , new Signal ());
		}
	}

	public void DetachReceiver(String key , BasicEventHandler<Dictionary<String , SysObj>> val)
	{
		lock (Sync)
		{
			if (!this.signals.ContainsKey (key))
			{
				return;
			}

			this.signals [key].Receiver -= val;
		}
	}

	public void DetachSignal(String key)
	{
		lock (Sync)
		{
			if (!this.signals.ContainsKey (key))
			{
				return;
			}

			this.signals [key].DetachParametersAll ();
			this.signals [key].DetachReceiversAll ();
			this.signals.Remove (key);
		}
	}

	public void DispatchSignal(String key)
	{
		if (!this.signals.ContainsKey (key))
		{
			return;
		}

		this.signals [key].Dispatch ();
	}

	public void DispatchSignal(String key , Dictionary<String , SysObj> val)
	{
		if (!this.signals.ContainsKey (key))
		{
			return;
		}

		this.signals [key].Dispatch (val);
	}
}