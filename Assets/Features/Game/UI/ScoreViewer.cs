using TMPro;
using UnityEngine;

namespace Project
{
	public class ScoreViewer : MonoBehaviour
	{
		[Header("Score")]
		[ContextMenuItem("Find Score Manager", nameof(FindScoreManager))]
		[Tooltip("If not set, it will try to be set on Awake")]
		[SerializeField] private ScoreManager _scoreManager;
		[SerializeField] private TMP_Text _scoreText;
		[SerializeField] private string _scoreFormat = "Score: {0}";

		[Header("General")]
		[SerializeField] private bool _setupOnEnable = true;

		private void Reset()
		{
			FindScoreManager();
			_scoreText = GetComponentInChildren<TMP_Text>();
		}

		private void Awake()
		{
			if (!_scoreManager)
				FindScoreManager();
		}

		private void OnEnable()
		{
			if (_setupOnEnable)
				Setup();
		}

		[ContextMenu("Find Score Manager")]
		private void FindScoreManager()
		{
			_scoreManager = FindFirstObjectByType<ScoreManager>();
		}

		private void Setup()
		{
			if (!_scoreManager || !_scoreText)
			{
				Debug.LogWarning("Must set Score Manager and Score Text.", this);
				return;
			}

			_scoreText.text = string.Format(_scoreFormat, _scoreManager.Score);
		}
	}
}
