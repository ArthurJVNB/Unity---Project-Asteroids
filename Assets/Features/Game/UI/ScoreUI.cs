using TMPro;
using UnityEngine;

namespace Project
{
	public class ScoreUI : MonoBehaviour
	{
		[SerializeField] private TMP_Text _scoreText;
		[SerializeField] private string _scoreFormat = "Score: {0}";

		[Header("Score Increment Text")]
		[Tooltip("Optional parent")]
		[SerializeField] private RectTransform _contentScorePopupText;
		[SerializeField] private PopupText _prefabScorePopupText;
		[SerializeField] private string _popupTextFormat = "+{0}";

		[Header("Events Listened")]
		[SerializeField] private ScoreEventData _scoreChangedEvent;

		private void Reset() => _scoreText = GetComponentInChildren<TMP_Text>();

		private void OnEnable() => _scoreChangedEvent.Event += OnScoreChanged_Event;
		private void OnDisable() => _scoreChangedEvent.Event -= OnScoreChanged_Event;

		private void Start() => SetText(0);

		private void OnScoreChanged_Event(ScoreData data)
		{
			SetText(data.ScoreTotal);
			ShowScorePopup(data);
		}

		private void SetText(int score)
		{
			_scoreText.text = string.Format(_scoreFormat, score);
		}

		private void ShowScorePopup(ScoreData data)
		{
			PopupText.Create(_prefabScorePopupText, _contentScorePopupText, string.Format(_popupTextFormat, data.ScoreIncrement), 2, data.AsteroidPosition);
		}
	}
}
