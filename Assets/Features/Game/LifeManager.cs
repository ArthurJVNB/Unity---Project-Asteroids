using System.Collections;
using UnityEngine;

namespace Project
{
	public class LifeManager : MonoBehaviour
	{
		[SerializeField] private float _timeToRespawn = 1;
		[Min(0)]
		[SerializeField] private int _lifes = 3;

		[Header("Events Listened")]
		[SerializeField] private SpaceshipEventData _spaceshipDiedEvent;
		[SerializeField] private IntEventData _lifesChangedEvent;

		private bool _isGameOver;

		private void OnEnable()
		{
			_spaceshipDiedEvent.Event += SpaceshipDiedEvent_Event;
		}

		private void Start()
		{
			_lifesChangedEvent.Invoke(_lifes);
		}

		private void OnDisable()
		{
			_spaceshipDiedEvent.Event -= SpaceshipDiedEvent_Event;
		}

		private void SpaceshipDiedEvent_Event(Spaceship spaceship)
		{
			Debug.Log("Spaceship died");
			DecreaseLife();
			StartCoroutine(HandleSpacheshipDeathRoutine(spaceship));
		}

		private IEnumerator HandleSpacheshipDeathRoutine(Spaceship spaceship)
		{
			spaceship.Disable();
			if (_isGameOver) yield break;
			yield return new WaitForSeconds(_timeToRespawn);
			spaceship.Enable();
			spaceship.StartGraceTimeAfterSpawn();
		}

		private void DecreaseLife()
		{
			_lifes--;
			
			if (_lifes < 0)
				GameOver();

			_lifesChangedEvent.Invoke(_lifes);
		}

		private void GameOver()
		{
			Debug.Log("<color=red>GAME OVER</color>");
			_isGameOver = true;
		}
	}
}
