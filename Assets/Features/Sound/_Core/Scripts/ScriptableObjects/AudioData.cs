using UnityEngine;

namespace Project.Sound
{
	[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/Sound System/Audio Data")]
	public class AudioData : AAudioData
	{
		[SerializeField] private SoundType _soundType = SoundType.SFX;

		[SerializeField] private AudioClip _audioClip;

		[Range(AudioSystem.MinVolume, AudioSystem.MaxVolume)]
		[SerializeField] private float _volume = AudioSystem.MaxVolume;

		[Range(AudioSystem.MinPitch, AudioSystem.MaxPitch)]
		[SerializeField] private float _pitch = AudioSystem.DefaultPitch;

		[Tooltip("Pitch deviation range. Zero means no change to the original pitch (which is 1).\n\n" +
			"When it has a value different of 0, it will deviate randomly from the default pitch by a value inside the range setted up here.")]
		[Range(0, 3)]
		[SerializeField] private float _pitchDeviationRange = 0;

		public SoundType SoundType
		{
			get => _soundType;
			set => _soundType = value;
		}

		public AudioClip AudioClip
		{
			get => _audioClip;
			set => _audioClip = value;
		}

		public float Volume
		{
			get => _volume;
			set => _volume = Mathf.Clamp(value, AudioSystem.MinVolume, AudioSystem.MaxVolume);
		}

		public float Pitch
		{
			get => _pitch;
			set => _pitch = Mathf.Clamp(value, AudioSystem.MinPitch, AudioSystem.MaxPitch);
		}

		public float PitchDeviation
		{
			get => _pitchDeviationRange;
			set => _pitchDeviationRange = value;
		}

		public override void Play(AudioSource audioSource, bool ignoreMuted = false)
		{
			AudioSystem.PlaySound(this, audioSource, ignoreMuted: ignoreMuted);
		}

		public override void PlayOneShot(Vector3 position, bool ignoreMuted = false)
		{
			AudioSystem.PlaySoundOneShot(this, position, ignoreMuted: ignoreMuted);
		}
	}
}
