using UnityEngine;

namespace Project.Sound
{
	public class SwitchSFX : MonoBehaviour
	{
		[SerializeField] private AudioData _audioDataOn;
		[SerializeField] private AudioData _audioDataOff;
		[Range(0, 1)]
		[SerializeField] private float _volume = 1;
		[Tooltip("If true, the sound will play even if the sound type is muted.")]
		[SerializeField] private bool _ignoreMuted;

		public void PlaySoundOn()
		{
			PlaySound(_audioDataOn);
		}

		public void PlaySoundOff()
		{
			PlaySound(_audioDataOff);
		}

		private void PlaySound(AudioData audioData)
		{
			AudioSystem.PlaySound(audioData, _volume, _ignoreMuted);
		}
	}
}
