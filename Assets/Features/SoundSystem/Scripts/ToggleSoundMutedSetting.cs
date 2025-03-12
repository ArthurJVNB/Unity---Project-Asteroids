using UnityEngine;

namespace Project.Sound
{
	public class ToggleSoundMutedSetting : ToggleSound
	{
		[SerializeField] private SoundType _muteSoundType;

		protected override void OnEnable()
		{
			base.OnEnable();
			_toggle.SetIsOnWithoutNotify(!AudioSystem.IsMute(_muteSoundType));
		}

		protected override void OnToggleValueChanged(bool isOn)
		{
			base.OnToggleValueChanged(isOn);
			AudioSystem.SetMuted(_muteSoundType, !isOn);
		}
	}
}
