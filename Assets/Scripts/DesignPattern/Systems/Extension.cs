using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{

	public static T GetComponentInChildren<T> (this Component source , Boolean includeInactive , Boolean includeSubChildren) where T : Component
	{
		var transform = source.transform;
		var component = (T) null;

		for (var c = 0 ; c < transform.childCount ; c++)
		{
			var current = transform.GetChild (c);

			if (!current.gameObject.activeInHierarchy && !includeInactive)
			{
				continue;
			}

			component = current.GetComponent<T> ();

			if (!component && (current.childCount > 0 && includeSubChildren))
			{
				component = current.GetComponentInChildren<T> (includeInactive , includeSubChildren);
			}

			if (component)
			{
				break;
			}
		}

		return component;
	}

	public static T [] GetComponentsInChildren<T> (this Component source , Boolean includeInactive , Boolean includeSubChildren) where T : Component
	{
		var transform = source.transform;
		var components = new List<T> ();

		for (var c = 0 ; c < transform.childCount ; c++)
		{
			var current = transform.GetChild (c);
			var component = (T) null;

			if (!current.gameObject.activeInHierarchy && !includeInactive)
			{
				continue;
			}

			component = current.GetComponent<T> ();

			if (component)
			{
				components.Add (component);
			}

			if (current.childCount > 0 && includeSubChildren)
			{
				components.AddRange (current.GetComponentsInChildren<T> (includeSubChildren , includeInactive));
			}
		}

		return components.ToArray ();
	}

	public static Transform Find (this Transform source , String name , Boolean includeSubChildren)
	{
		var result = (Transform) null;

		for (var c = 0 ; c < source.childCount ; c++)
		{
			var current = source.GetChild (c);

			if (String.CompareOrdinal (current.name , name) == 0)
			{
				result = current;
			}

			if (!result && (current.childCount > 0 && includeSubChildren))
			{
				result = current.Find (name , includeSubChildren);
			}

			if (result)
			{
				break;
			}
		}

		return result;
	}
}