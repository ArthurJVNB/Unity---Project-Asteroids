using UnityEngine;

namespace Project
{
	public class ScoreManager : MonoBehaviour
	{
		[Min(0)]
		[SerializeField] private float _scoreMultiplier = 1;
		[SerializeField] private int _score = 0;

		[Header("Events Listened")]
		[SerializeField] private AsteroidEventData _asteroidDestroyedEvent;

		[Header("Events Invoked")]
		[SerializeField] private IntEventData _scoreChangedTotalEvent;
		[SerializeField] private IntEventData _scoreIncrementEvent;

		private void OnEnable()
		{
			_asteroidDestroyedEvent.Event += OnAsteroidDestroyed_Event;
		}

		private void OnDisable()
		{
			_asteroidDestroyedEvent.Event -= OnAsteroidDestroyed_Event;
		}

		private void OnAsteroidDestroyed_Event(Asteroid asteroid)
		{
			int add = Mathf.RoundToInt(_scoreMultiplier * asteroid.MaxSize / asteroid.Size);
			Debug.Log($"<color=white>score {add}</color>");
			_score += add;
			_scoreChangedTotalEvent.Invoke(_score);
			_scoreIncrementEvent.Invoke(add);
		}

		//private void OnGUI()
		//{
		//	GUI.Label(new Rect(10, 10, 100, 20), $"Score: {_score}");
		//}
	}
}
