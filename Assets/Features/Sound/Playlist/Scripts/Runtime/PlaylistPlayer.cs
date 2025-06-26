using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Project.Sound.Playlist
{
	public class PlaylistPlayer : MonoBehaviour
	{
		[SerializeField] private AudioSource _audioSource;
		[SerializeField] private AudioDataContainer _playlist;
		[SerializeField, ReadOnly] private List<AudioData> _currentAudioDatas;
		[ShowNonSerializedField] private int _currentIndex = -1;

		private bool HasPlaylist
		{
			get => _playlist != null && _playlist.AudioDatas != null && _playlist.AudioDatas.Length > 0;
		}

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

		//Play (toca a playlist, se não estiver tocando. também cria a lista aleatória caso ainda não exista)
		public void Play()
		{
			if (IsPlayingCurrentAudioData) return;
			if (!IsPlaylistReady)
				Shuffle();
			CurrentAudioData.Play(_audioSource);
		}

		//Pause (pausa se tiver uma playlist tocando)
		public void Pause()
		{
			if (!IsPlaying) return;
			_audioSource.Pause();
		}

		//Stop (para completamente a playlist e reseta a lista aleatória criada) -> ou talvez não resete a lista, apenas o índice do que está sendo tocado?
		public void Stop()
		{
			_audioSource.Stop();
			//Shuffle();
			//_currentIndex = 0;
		}

		//Shuffle (embaralha a lista aleatória criada. reseta o índice do que está sendo tocado)
		public void Shuffle()
		{
			Debug.LogWarning("Shuffle was not implemented. It will only copy the playlist to the current audio datas and reset the index.");
			_currentIndex = 0;
			_currentAudioDatas = new List<AudioData>(_playlist.AudioDatas);
			if (IsPlaying)
				Stop();
		}

		//Next (muda música atual para o próximo da lista aleatória. se for o último, muda para o primeiro)
		public void Next()
		{
			if (!IsPlaylistReady)
				Shuffle();

			_currentIndex = (_currentIndex + 1) % _currentAudioDatas.Count;

			if (IsPlaying)
				Play();
		}

		//Previous (muda música atual para o anterior da lista aleatória. se for o primeiro, muda para o último)
		public void Previous()
		{
			if (!IsPlaylistReady)
				Shuffle();

			_currentIndex = _currentIndex <= 0 ? _currentAudioDatas.Count - 1 : (_currentIndex - 1) % _currentAudioDatas.Count;

			if (IsPlaying)
				Play();
		}

		//PlayNext (se for o último, toca o primeiro. se não tiver tocando, funcionará como Play)
		public void PlayNext()
		{
			Next();
			Play();
		}

		//PlayPrevious (se for o primeiro, toca o último. se não tiver tocando, funcionará como Play)
		public void PlayPrevious()
		{
			Previous();
			Play();
		}
	}
}
