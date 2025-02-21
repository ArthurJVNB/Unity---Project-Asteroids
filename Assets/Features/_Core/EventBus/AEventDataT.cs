using System;
using UnityEngine;

namespace Project
{
	public abstract class AEventDataT<T> : ScriptableObject
	{
		protected const string ScriptablePath = "Scriptable Objects/Event Bus/";

		public event Action<T> Event = _ => { };

		public void Invoke(T item) => Event.Invoke(item);
	}
}
