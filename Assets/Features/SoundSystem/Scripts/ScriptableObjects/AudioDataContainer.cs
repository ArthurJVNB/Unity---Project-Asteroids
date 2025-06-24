using UnityEngine;

namespace Project.Sound
{
	[CreateAssetMenu(fileName = "AudioDataContainer", menuName = "Scriptable Objects/Audio System/Audio Data Container")]
	public class AudioDataContainer : AAudioData
	{
		[SerializeField] private AudioData[] _audioDatas;

		public AudioData[] AudioDatas { get => _audioDatas; set => _audioDatas = value; }

		public AudioData RandomAudioData => _audioDatas[Random.Range(0, _audioDatas.Length)];

		public override void Play(AudioSource audioSource, bool ignoreMuted = false)
		{
			AudioSystem.PlaySound(RandomAudioData, audioSource, ignoreMuted: ignoreMuted);
		}

		public override void PlayOneShot(Vector3 position, bool ignoreMuted = false)
		{
			AudioSystem.PlaySoundOneShot(RandomAudioData, position, ignoreMuted: ignoreMuted);
		}
	}
}
