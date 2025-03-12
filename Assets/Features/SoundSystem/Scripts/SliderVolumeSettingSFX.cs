using UnityEngine;

namespace Project.Sound
{
	public class SliderVolumeSettingSFX : SliderSFX
	{
		protected override void OnSliderValueChanged(float value)
		{
			base.OnSliderValueChanged(value);
			AudioSystem.SetVolume(_audioData.SoundType, _slider.normalizedValue);
		}
	}
}
