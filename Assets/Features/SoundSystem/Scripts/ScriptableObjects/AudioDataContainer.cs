using UnityEngine;

namespace Project.Sound
{
	[CreateAssetMenu(fileName = "AudioDataContainer", menuName = "Scriptable Objects/Audio System/Audio Data Container")]
	public class AudioDataContainer : ScriptableObject
	{
		[SerializeField] private AudioData[] _audioDatas;

		public AudioData[] AudioDatas { get => _audioDatas; set => _audioDatas = value; }

		public AudioData RandomAudioData => _audioDatas[Random.Range(0, _audioDatas.Length)];
	}
}
