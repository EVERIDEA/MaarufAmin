using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using SysObj = System.Object;

public class SignalEmitter : BaseBehaviour
{
	public void Emit (String identifier)
	{
		if (String.IsNullOrEmpty (identifier) || String.IsNullOrWhiteSpace (identifier))
		{
			return;
		}

		var data = identifier.Split (new [] { ":" } , StringSplitOptions.RemoveEmptyEntries);
		var compiled = new Dictionary<String , SysObj> ();

		if (data.Length > 1)
		{
			var parameters = data [1].Split (new [] { "," } , StringSplitOptions.RemoveEmptyEntries);

			for (var c = 0 ; c < parameters.Length ; c++)
			{
				var param = parameters [c].Split (new [] { "=" } , StringSplitOptions.RemoveEmptyEntries);
				var key = param [0];
				// TODO : Update / Create function to automatically parse the data type
				var val = param [1];
				compiled.Add (key , val);
			}
		}

		SignalManager.Instance.DispatchSignal (data [0] , compiled);
	}
}