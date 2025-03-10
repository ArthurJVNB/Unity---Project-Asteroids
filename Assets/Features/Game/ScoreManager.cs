using UnityEngine;

namespace Project
{
	public class ScoreManager : MonoBehaviour
	{
		[Min(0)]
		[SerializeField] private float _scoreMultiplier = 1;
		[SerializeField] private int _score = 0;

		[Header("Events Invoked")]
		[SerializeField] private ScoreEventData _scoreChangedEvent;

		[Header("Events Listened")]

		[SerializeField] private AsteroidEventData _asteroidDestroyedEvent;
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
			_scoreChangedEvent.Invoke(new ScoreData(_score, add, asteroid.transform.position));
		}

		//private void OnGUI()
		//{
		//	GUI.Label(new Rect(10, 10, 100, 20), $"Score: {_score}");
		//}
	}
}
