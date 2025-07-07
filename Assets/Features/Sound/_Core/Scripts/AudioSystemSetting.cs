using UnityEngine;

namespace Project.Sound
{
	public class AudioSystemSetting : MonoBehaviour
	{
		[SerializeField] private SoundType _soundType = SoundType.SFX;

		public void SetVolume(float volume)
		{
			AudioSystem.SetVolume(_soundType, volume);
		}

		public void SetMuted(bool muted)
		{
			AudioSystem.SetMuted(_soundType, muted);
		}

		public void SetNotMuted(bool notMuted)
		{
			SetMuted(!notMuted);
		}
	}
}
