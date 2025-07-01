using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Sound.Playlist
{
	public class PlaylistPlayer : MonoBehaviour
	{
		[SerializeField] private AudioSource _audioSource;
		[SerializeField] private AudioDataContainer _playlist;
		[SerializeField, Min(0)] private float _timeBetweenAudiosMin = .1f;
		[SerializeField, Min(0)] private float _timeBetweenAudiosMax = 5f;
		[SerializeField] private bool _shuffle = true;
		[Tooltip("If set to true, the player will loop through the playlist indefinitely. " +
				 "If set to false, it will stop playing when it reaches the end of the playlist.\n\n" +
				 "Note: Only applicable in transition.")]
		[SerializeField] private bool _loop = true;
		private State _currentState = State.None;
		private List<AudioData> _currentAudioDatas;
		private int _currentIndex = -1;
		private Coroutine _transitionRoutine;

		public enum State
		{
			None,
			Playing,
			Paused,
			InTransition,
			InTransitionPaused,
		}

		public State CurrentState => _currentState;

		public AudioData CurrentAudioData
		{
			get
			{
				bool hasAudioDatas = _currentAudioDatas != null && _currentAudioDatas.Count > 0;
				bool hasValidIndex = _currentIndex >= 0 && _currentIndex < _currentAudioDatas.Count;
				return hasAudioDatas && hasValidIndex ? _currentAudioDatas[_currentIndex] : null;
			}
		}

		private bool IsPlaying
		{
			get
			{
				//return _audioSource.isPlaying;
				return _currentState == State.Playing;
			}
		}

		private bool IsPlayingCurrentAudioData => _audioSource.clip && CurrentAudioData.AudioClip == _audioSource.clip && IsPlaying;

		private bool IsPlaylistReady => _currentAudioDatas != null && _currentAudioDatas.Count > 0;

#if UNITY_EDITOR
		private float _previousTimeBetweenAudiosMin = 0f;
		private float _previousTimeBetweenAudiosMax = 0f;
		private void OnValidate()
		{
			bool isMovingMin = false;
			bool isMovingMax = false;

			if (_previousTimeBetweenAudiosMin != _timeBetweenAudiosMin)
			{
				isMovingMin = true;
			}

			if (_previousTimeBetweenAudiosMax != _timeBetweenAudiosMax)
			{
				isMovingMax = true;
			}

			if (isMovingMin)
			{
				if (_timeBetweenAudiosMin > _timeBetweenAudiosMax)
					_timeBetweenAudiosMax = _timeBetweenAudiosMin;
			}
			else if (isMovingMax)
			{
				if (_timeBetweenAudiosMax < _timeBetweenAudiosMin)
					_timeBetweenAudiosMin = _timeBetweenAudiosMax;
			}

			_previousTimeBetweenAudiosMin = _timeBetweenAudiosMin;
			_previousTimeBetweenAudiosMax = _timeBetweenAudiosMax;
		}
#endif

		private void Update()
		{
			HandleStateTransition();
		}

		public void Play()
		{
			if (IsPlayingCurrentAudioData) return;
			
			ValidatePlaylist();
			
			if (_currentState == State.InTransitionPaused)
			{
				_currentState = State.InTransition;
				return;
			}

			CurrentAudioData.Play(_audioSource);
			_currentState = State.Playing;
		}

		public void Pause()
		{
			if (_currentState == State.Paused || _currentState == State.InTransitionPaused) return;
			_audioSource.Pause();
			_currentState = _currentState == State.InTransition ? State.InTransitionPaused : State.Paused;
		}

		public void Stop()
		{
			_audioSource.Stop();
			_currentState = State.None;
		}

		public void Next()
		{
			ValidatePlaylist();

			_currentIndex = (_currentIndex + 1) % _currentAudioDatas.Count;

			if (IsPlaying || _currentState == State.InTransition)
				Play();
		}

		public void Previous()
		{
			ValidatePlaylist();

			_currentIndex = _currentIndex <= 0 ? _currentAudioDatas.Count - 1 : (_currentIndex - 1) % _currentAudioDatas.Count;

			if (_currentState == State.Playing || _currentState == State.InTransition)
				Play();
		}

		public void PlayNext()
		{
			Next();
			Play();
		}

		public void PlayPrevious()
		{
			Previous();
			Play();
		}

		public void Shuffle()
		{
			Stop();
			_currentIndex = 0;
			var random = new System.Random();
			_currentAudioDatas = _playlist.AudioDatas.OrderBy(_ => random.Next()).ToList();
		}

		private void ValidatePlaylist()
		{
			if (IsPlaylistReady) return;
			BuildPlaylist();
		}

		private void BuildPlaylist()
		{
			if (_shuffle)
			{
				Shuffle();
				return;
			}

			Stop();
			_currentIndex = 0;
			_currentAudioDatas = new List<AudioData>(_playlist.AudioDatas);
		}

		private void HandleStateTransition()
		{
			if (_currentState == State.Playing && !_audioSource.isPlaying)
				StartTransition();
		}

		private void StartTransition()
		{
			_currentState = State.None;
			Next();

			if (!_loop && _currentIndex == 0)
				return;

			if (_timeBetweenAudiosMin <= 0 && _timeBetweenAudiosMax <= 0)
			{
				Play();
				return;
			}

			if (_transitionRoutine != null)
				StopCoroutine(_transitionRoutine);
			_transitionRoutine = StartCoroutine(TransitionRoutine());
		}

		private IEnumerator TransitionRoutine()
		{
			_currentState = State.InTransition;

			float waitTime = 0;
			float totalWaitTime = Random.Range(_timeBetweenAudiosMin, _timeBetweenAudiosMax);

			while (waitTime < totalWaitTime)
			{
				if (_currentState == State.InTransitionPaused)
					yield return new WaitWhile(() => _currentState == State.InTransitionPaused);
				yield return null;
				waitTime += Time.deltaTime;
			}

			if (_currentState != State.InTransition) yield break;
			Play();
		}
	}
}
