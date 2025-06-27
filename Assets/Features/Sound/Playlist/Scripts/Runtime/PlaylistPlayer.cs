using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Project.Sound.Playlist
{
	public class PlaylistPlayer : MonoBehaviour
	{
		[SerializeField] private AudioSource _audioSource;
		[SerializeField] private AudioDataContainer _playlist;
		[SerializeField] private bool _shuffle = true;
		[SerializeField, Min(0)] private float _timeBetweenAudiosMin = .1f;
		[SerializeField, Min(0)] private float _timeBetweenAudiosMax = 5f;
		private List<AudioData> _currentAudioDatas;
		private int _currentIndex = -1;

		private bool IsPlaying => _audioSource.isPlaying;

		private bool IsPlayingCurrentAudioData => _audioSource.clip && CurrentAudioData.AudioClip == _audioSource.clip && IsPlaying;

		private bool IsPlaylistReady => _currentAudioDatas != null && _currentAudioDatas.Count > 0;

		public AudioData CurrentAudioData
		{
			get
			{
				bool hasAudioDatas = _currentAudioDatas != null && _currentAudioDatas.Count > 0;
				bool hasValidIndex = _currentIndex >= 0 && _currentIndex < _currentAudioDatas.Count;
				return hasAudioDatas && hasValidIndex ? _currentAudioDatas[_currentIndex] : null;
			}
		}

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
			if (IsPlaying) return;
		}

		public void Play()
		{
			if (IsPlayingCurrentAudioData) return;
			ValidatePlaylist();
			CurrentAudioData.Play(_audioSource);
		}

		public void Pause()
		{
			if (!IsPlaying) return;
			_audioSource.Pause();
		}

		public void Stop()
		{
			_audioSource.Stop();
		}

		public void Next()
		{
			ValidatePlaylist();

			_currentIndex = (_currentIndex + 1) % _currentAudioDatas.Count;

			if (IsPlaying)
				Play();
		}

		public void Previous()
		{
			ValidatePlaylist();

			_currentIndex = _currentIndex <= 0 ? _currentAudioDatas.Count - 1 : (_currentIndex - 1) % _currentAudioDatas.Count;

			if (IsPlaying)
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
	}
}
