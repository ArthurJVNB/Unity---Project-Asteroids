using UnityEngine;

namespace Project.Sound
{
	[CreateAssetMenu(fileName = "AudioDataArray", menuName = "Scriptable Objects/Sound System/Audio Data Array")]
	public class AudioDataArray : AAudioData
	{
		private const int MinPitchDeviationRange = 0;
		private const int MaxPitchDeviationRange = 3;

		[SerializeField] private SoundType _soundType = SoundType.SFX;

		[Range(AudioSystem.MinVolume, AudioSystem.MaxVolume)]
		[SerializeField] private float _volume = AudioSystem.MaxVolume;

		[Range(AudioSystem.MinPitch, AudioSystem.MaxPitch)]
		[SerializeField] private float _pitch = AudioSystem.DefaultPitch;

		[Tooltip("Pitch deviation range. Zero means no change to the original pitch (which is 1).\n\n" +
			"When it has a value different of 0, it will deviate randomly from the default pitch by a value inside the range setted up here.")]
		[Range(MinPitchDeviationRange, MaxPitchDeviationRange)]
		[SerializeField] private float _pitchDeviationRange = 0;

		[SerializeField] private AudioClip[] _audioClips;

		public SoundType SoundType
		{
			get => _soundType;
			set => _soundType = value;
		}

		public AudioClip[] AudioClips
		{
			get => _audioClips;
			set => _audioClips = value;
		}

		public float Volume
		{
			get => _volume;
			set => _volume = Mathf.Clamp(value, AudioSystem.MinVolume, AudioSystem.MaxVolume);
		}

		public float PitchDeviation
		{
			get => _pitchDeviationRange;
			set => _pitchDeviationRange = Mathf.Clamp(value, MinPitchDeviationRange, MaxPitchDeviationRange);
		}

		public AudioClip RandomAudioClip => _audioClips[Random.Range(0, _audioClips.Length)];

		private float RandomPitch => AudioSystem.GetRandomPitch(_pitch, _pitchDeviationRange);

		public override void Play(AudioSource audioSource, bool ignoreMuted = false)
		{
			AudioSystem.PlaySound(RandomAudioClip, _soundType, audioSource, pitch: RandomPitch, volume: _volume, ignoreMuted: ignoreMuted);
		}

		public override void PlayOneShot(Vector3 position, bool ignoreMuted = false)
		{
			AudioSystem.PlaySoundOneShot(RandomAudioClip, _soundType, position, pitch: RandomPitch, volume: _volume, ignoreMuted: ignoreMuted);
		}
	}
}
