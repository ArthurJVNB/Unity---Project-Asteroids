using System;
using UnityEngine;

namespace Project
{
	[CreateAssetMenu(fileName = "EventData", menuName = "Scriptable Objects/Event Bus/Event (parameterless)")]
	public class EventData : ScriptableObject
	{
		public event Action Event = () => { };

		public void Invoke() => Event.Invoke();
	}
}
