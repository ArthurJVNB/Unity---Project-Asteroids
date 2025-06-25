using UnityEngine;
using UnityEngine.UI;

namespace Project.Sound
{
	public class ToggleSound : SwitchSound
	{
		[Space]
		[SerializeField] protected Toggle _toggle;

		private void Reset()
		{
			_toggle = GetComponentInChildren<Toggle>();
		}

		protected virtual void OnEnable()
		{
			_toggle.onValueChanged.AddListener(OnToggleValueChanged);
		}

		private void OnDisable()
		{
			_toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
		}

		protected virtual void OnToggleValueChanged(bool isOn)
		{
			if (isOn)
				PlaySoundOn();
			else
				PlaySoundOff();
		}
	}
}
