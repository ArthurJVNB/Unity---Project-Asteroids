using System.Collections.Generic;
using UnityEngine;

namespace Project.Sound
{
	public static class AudioSystem
	{
		public const int DefaultVolume = 1;
		private const bool DefaultMuted = false;

		private static Dictionary<SoundType, float> _volumeLevels;
		private static Dictionary<SoundType, bool> _muted;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
			SetupDefaultVolumes();
			SetupDefaultMuted();
		}

		public static void SetVolume(SoundType soundType, float volume)
		{
			_volumeLevels[soundType] = Mathf.Clamp01(volume);
		}

		public static float GetVolume(SoundType soundType)
		{
			return _volumeLevels[soundType] * (_muted[soundType] ? 0 : 1);
		}

		public static float GetVolumeIgnoreMuted(SoundType soundType)
		{
			return _volumeLevels[soundType];
		}

		public static void SetMuted(SoundType soundType, bool isMuted)
		{
			_muted[soundType] = isMuted;
		}

		public static bool IsMute(SoundType soundType)
		{
			return _muted[soundType];
		}

		public static void PlaySound(AudioData audioData, Vector3 position, float volume = DefaultVolume, bool ignoreMuted = false)
		{
			GameObject gameObject = new GameObject("One shot audio");
			gameObject.transform.position = position;
			AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
			audioSource.clip = audioData.AudioClip;
			audioSource.spatialBlend = 0f;
			audioSource.volume = volume * (ignoreMuted ? GetVolumeIgnoreMuted(audioData.SoundType) : GetVolume(audioData.SoundType));
			audioSource.Play();
			//Object.Destroy(gameObject, audioData.AudioClip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
			Object.Destroy(gameObject, Mathf.Max(audioData.AudioClip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale), .5f));
		}

		public static void PlaySound(AudioData audioData, float volume = DefaultVolume, bool ignoreMuted = false)
		{
			PlaySound(audioData, Vector3.zero, volume, ignoreMuted);
		}

		private static void SetupDefaultVolumes()
		{
			_volumeLevels = new Dictionary<SoundType, float>();
			foreach (SoundType soundType in System.Enum.GetValues(typeof(SoundType)))
				_volumeLevels[soundType] = DefaultVolume;
		}

		private static void SetupDefaultMuted()
		{
			_muted = new Dictionary<SoundType, bool>();
			foreach (SoundType soundType in System.Enum.GetValues(typeof(SoundType)))
				_muted[soundType] = DefaultMuted;
		}
	}
}
