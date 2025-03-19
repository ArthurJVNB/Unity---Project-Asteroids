using System;
using UnityEngine;

namespace Project
{
	[CreateAssetMenu(fileName = "EventData", menuName = "Scriptable Objects/Event Bus/Event (parameterless)")]
	public class EventData : ScriptableObject
	{
		public event Action Event = () => { };

		/// <summary>
		/// Invokes the event.
		/// <para>Note: It is safe to invoke the event even if there are no subscribers.</para>
		/// </summary>
		public void Invoke() => Event.Invoke();
	}
}
