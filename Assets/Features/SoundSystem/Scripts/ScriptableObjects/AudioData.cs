using UnityEngine;

namespace Project.Sound
{
	[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/Audio System/Audio Data")]
	public class AudioData : ScriptableObject
	{
		[SerializeField] private SoundType _soundType = SoundType.SFX;
		[SerializeField] private AudioClip _audioClip;

		public SoundType SoundType { get => _soundType; set => _soundType = value; }
		public AudioClip AudioClip { get => _audioClip; set => _audioClip = value; }
	}
}
