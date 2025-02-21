using UnityEngine;

namespace Project
{
	public class LevelManager : MonoBehaviour
	{
		[SerializeField] private SpaceshipEventData _spaceshipDiedEvent;

		private void OnEnable()
		{
			_spaceshipDiedEvent.Event += SpaceshipDiedEvent_Event;
		}

		private void SpaceshipDiedEvent_Event(Spaceship spaceship)
		{
			Debug.Log("Spaceship died");
		}
	}
}
