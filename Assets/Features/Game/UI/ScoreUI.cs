using TMPro;
using UnityEngine;

namespace Project
{
	public class ScoreUI : MonoBehaviour
	{
		[SerializeField] private TMP_Text _scoreText;
		[SerializeField] private string _scoreFormat = "Score: {0}";

		[Header("Events Listened")]
		[SerializeField] private IntEventData _scoreChangedTotalEvent;

		private void OnEnable() => _scoreChangedTotalEvent.Event += OnScoreChangedTotal_Event;
		private void OnDisable() => _scoreChangedTotalEvent.Event -= OnScoreChangedTotal_Event;

		private void Start() => SetText(0);
		private void OnScoreChangedTotal_Event(int score) => SetText(score);

		private void SetText(int score)
		{
			_scoreText.text = string.Format(_scoreFormat, score);
		}
	}
}
