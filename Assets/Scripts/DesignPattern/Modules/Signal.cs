using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using SysObj = System.Object;

public class Signal
{
	private static readonly SysObj Sync = new SysObj ();

	private Dictionary<String , SysObj> parameters = new Dictionary<String , SysObj> ();

	private event BasicEventHandler<Dictionary<String , SysObj>> receiver;

	public event BasicEventHandler<Dictionary<String , SysObj>> Receiver
	{
		add
		{
			lock (Sync)
			{
				this.receiver -= value;
				this.receiver += value;
			}
		}
		remove
		{
			lock (Sync)
			{
				this.receiver -= value;
			}
		}
	}

	public void AttachParameter(String key , SysObj val)
	{
		lock (Sync)
		{
			if (this.parameters.ContainsKey (key))
			{
				return;
			}

			this.parameters.Add (key , val);
		}
	}

	public void AttachParameters(Dictionary<String , SysObj> parameters)
	{
		foreach (var entry in parameters)
		{
			this.AttachParameter (entry.Key , entry.Value);
		}
	}

	public void DetachParameter(String key)
	{
		lock (Sync)
		{
			if (!this.parameters.ContainsKey (key))
			{
				return;
			}

			this.parameters.Remove (key);
		}
	}

	public void DetachParameters(List<String> keys)
	{
		foreach (var entry in keys)
		{
			this.DetachParameter (entry);
		}
	}

	public void DetachParametersAll()
	{
		lock (Sync)
		{
			this.parameters.Clear ();
		}
	}

	public void DetachReceiversAll()
	{
		lock (Sync)
		{
			this.receiver = null;
		}
	}

	public void Dispatch()
	{
		lock (Sync)
		{
			this.receiver?.Invoke (this.parameters);
			this.DetachParametersAll ();
		}
	}

	public void Dispatch(Dictionary<String , SysObj> parameters)
	{
		lock (Sync)
		{
			this.DetachParametersAll ();
			this.receiver?.Invoke (parameters);
		}
	}

	public void RetachParameter(String key , SysObj val)
	{
		lock (Sync)
		{
			if (!this.parameters.ContainsKey (key))
			{
				return;
			}

			this.parameters [key] = val;
		}
	}

	public void RetachParameters(Dictionary<String , SysObj> parameters)
	{
		foreach (var entry in parameters)
		{
			this.RetachParameter (entry.Key , entry.Value);
		}
	}
}