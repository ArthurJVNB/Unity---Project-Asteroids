using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Project
{
	public class LevelManager : MonoBehaviour
	{
		[SerializeField] private float _timeToRespawn = 1;
		[Min(0)]
		[SerializeField] private int _lifes = 3;

		[Header("Events")]
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
			HandleSpacheshipDeathAsync(spaceship);
		}

		private async Task HandleSpacheshipDeathAsync(Spaceship spaceship)
		{
			spaceship.Disable();
			if (_isGameOver) return;
			await Task.Delay((int)(1000 * _timeToRespawn));
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
