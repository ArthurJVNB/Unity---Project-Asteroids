using UnityEngine;

namespace Project.Sound
{
	[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/Audio System/Audio Data")]
	public class AudioData : ScriptableObject
	{
		[SerializeField] private SoundType _soundType = SoundType.SFX;

		[SerializeField] private AudioClip _audioClip;

		[Range(AudioSystem.MinPitch, AudioSystem.MaxPitch)]
		[SerializeField] private float _pitch = AudioSystem.DefaultPitch;

		[Tooltip("Pitch deviation range. Zero means no change to the original pitch (which is 1).\n\n" +
			"When it has a value different of 0, it will deviate randomly from the default pitch by a value inside the range setted up here.")]
		[Range(0, 3)]
		[SerializeField] private float _pitchDeviationRange = 0;

		public SoundType SoundType { get => _soundType; set => _soundType = value; }
		public AudioClip AudioClip { get => _audioClip; set => _audioClip = value; }
		public float Pitch { get => _pitch; set => _pitch = value; }
		public float PitchDeviation { get => _pitchDeviationRange; set => _pitchDeviationRange = value; }

		public void Play(AudioSource audioSource, bool ignoreMuted = false)
		{
			AudioSystem.PlaySound(this, audioSource, ignoreMuted: ignoreMuted);
		}

		public void PlayOneShot(Vector2 position, bool ignoreMuted = false)
		{
			AudioSystem.PlaySoundOneShot(this, position, ignoreMuted: ignoreMuted);
		}
	}
}
