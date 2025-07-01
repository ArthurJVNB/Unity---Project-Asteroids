using UnityEngine;

namespace Project.Sound
{
	public class SliderSoundVolumeSetting : SliderSound
	{
		protected override void OnSliderValueChanged(float value)
		{
			base.OnSliderValueChanged(value);
			AudioSystem.SetVolume(_soundType, _slider.normalizedValue);
		}
	}
}
