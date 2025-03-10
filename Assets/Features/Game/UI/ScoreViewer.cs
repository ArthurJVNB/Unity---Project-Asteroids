using TMPro;
using UnityEngine;

namespace Project
{
	public class ScoreViewer : MonoBehaviour
	{
		[Header("Score (optional)")]
		[SerializeField] private ScoreManager _scoreManager;
		[SerializeField] private TMP_Text _scoreText;
		[SerializeField] private string _scoreFormat = "Score: {0}";

		[Header("General")]
		[SerializeField] private bool _setupOnEnable = true;

		private void Reset()
		{
			_scoreManager = FindFirstObjectByType<ScoreManager>();
			_scoreText = GetComponentInChildren<TMP_Text>();
		}

		private void OnEnable()
		{
			if (_setupOnEnable)
				Setup();
		}

		private void Setup()
		{
			SetupScore();
		}

		private void SetupScore()
		{
			if (!_scoreManager || !_scoreText)
				return;

			_scoreText.text = string.Format(_scoreFormat, _scoreManager.Score);
		}
	}
}
