using UnityEngine;
using UnityEngine.UI;

namespace Project.Sound
{
	public class ToggleSFX : SwitchSFX
	{
		[Space]
		[SerializeField] private Toggle _toggle;

		private void Reset()
		{
			_toggle = GetComponentInChildren<Toggle>();
		}

		private void OnEnable()
		{
			_toggle.onValueChanged.AddListener(OnToggleValueChanged);
		}

		private void OnDisable()
		{
			_toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
		}

		private void OnToggleValueChanged(bool isOn)
		{
			if (isOn)
				PlaySoundOn();
			else
				PlaySoundOff();
		}
	}
}
