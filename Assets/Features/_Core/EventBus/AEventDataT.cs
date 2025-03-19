using System;
using UnityEngine;

namespace Project
{
	public abstract class AEventDataT<T> : ScriptableObject
	{
		protected const string ScriptablePath = "Scriptable Objects/Event Bus/";

		public event Action<T> Event = _ => { };

		/// <summary>
		/// Invokes the event.
		/// <para>Note: It is safe to invoke the event even if there are no subscribers.</para>
		/// </summary>
		/// <param name="item"></param>
		public void Invoke(T item) => Event.Invoke(item);
	}
}
