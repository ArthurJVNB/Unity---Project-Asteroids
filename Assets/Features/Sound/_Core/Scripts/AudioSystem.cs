using System.Collections.Generic;
using UnityEngine;

namespace Project.Sound
{
	public static class AudioSystem
	{
		public static event System.Action<SoundType, float> OnChangedVolume;
		public static event System.Action<SoundType, bool> OnChangedMuted;

		public const int DefaultVolume = 1;
		public const int DefaultPitch = 1;
		public const int MaxVolume = 1;
		public const int MinVolume = 0;
		public const int MaxPitch = 3;
		public const int MinPitch = -MaxPitch;
		private const bool DefaultMuted = false;

		private static Dictionary<SoundType, float> _volumeLevels;
		private static Dictionary<SoundType, bool> _muted;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
			OnChangedVolume = (_, _) => { };
			OnChangedMuted = (_, _) => { };
			SetupDefaultVolumes();
			SetupDefaultMuted();
		}

		public static void SetVolume(SoundType soundType, float volume)
		{
			_volumeLevels[soundType] = Mathf.Clamp01(volume);
			OnChangedVolume.Invoke(soundType, volume);
		}

		public static float GetVolume(AudioData audioData)
		{
			return GetVolume(audioData.SoundType) * audioData.Volume;
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
			OnChangedMuted.Invoke(soundType, isMuted);
		}

		public static bool IsMute(SoundType soundType)
		{
			return _muted[soundType];
		}

		public static float GetRandomPitch(float pitch, float pitchDeviationRange)
		{
			return Random.Range(pitch - pitchDeviationRange, pitch + pitchDeviationRange);
		}

		public static void PlaySoundOneShot(AudioClip audioClip, SoundType soundType, Vector3 position, float pitch = DefaultPitch, float volume = DefaultVolume, bool ignoreMuted = false)
		{
			GameObject gameObject = new("One shot audio");
			gameObject.transform.position = position;
			AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
			audioSource.clip = audioClip;
			audioSource.spatialBlend = 0f;
			audioSource.pitch = pitch;
			audioSource.volume = volume * (ignoreMuted ? GetVolumeIgnoreMuted(soundType) : GetVolume(soundType));
			audioSource.Play();
			//Object.Destroy(gameObject, audioData.AudioClip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
			Object.DontDestroyOnLoad(gameObject);
			Object.Destroy(gameObject, Mathf.Max(audioClip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale), .5f));
		}

		public static void PlaySoundOneShot(AudioClip audioClip, SoundType soundType, float pitch = DefaultPitch, float volume = DefaultVolume, bool ignoreMuted = false)
		{
			PlaySoundOneShot(audioClip, soundType, Vector3.zero, pitch, volume, ignoreMuted);
		}

		public static void PlaySound(AudioClip audioClip, SoundType soundType, AudioSource audioSource, float pitch = DefaultPitch, float volume = DefaultVolume, bool ignoreMuted = false)
		{
			audioSource.clip = audioClip;
			audioSource.volume = volume * (ignoreMuted ? GetVolumeIgnoreMuted(soundType) : GetVolume(soundType));
			audioSource.pitch = pitch;
			audioSource.Play();
		}

		public static void PlaySoundOneShot(AudioData audioData, Vector3 position, float volume = DefaultVolume, bool ignoreMuted = false)
		{
			PlaySoundOneShot(audioData.AudioClip, audioData.SoundType, position, GetRandomPitch(audioData.Pitch, audioData.PitchDeviation), volume, ignoreMuted);
		}

		public static void PlaySoundOneShot(AudioData audioData, float volume = DefaultVolume, bool ignoreMuted = false)
		{
			PlaySoundOneShot(audioData, Vector3.zero, volume, ignoreMuted);
		}

		public static void PlaySound(AudioData audioData, AudioSource audioSource, bool ignoreMuted = false)
		{
			PlaySound(audioData.AudioClip, audioData.SoundType, audioSource, GetRandomPitch(audioData.Pitch, audioData.PitchDeviation), audioData.Volume, ignoreMuted);
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
