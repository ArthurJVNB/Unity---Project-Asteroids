using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Sound
{
	public class SliderSFX : MonoBehaviour
	{
		[SerializeField] protected AudioData _audioData;
		[Range(0, 1)]
		[SerializeField] private float _volume = 1;
		[Tooltip("If true, the sound will play even if the sound type is muted.")]
		[SerializeField] private bool _ignoreMuted;

		[Space]
		[SerializeField] protected Slider _slider;
		[SerializeField] private AudioSource _audioSource;

		private bool _valueChanged;
		private float _timeElapsed;

		private void Reset()
		{
			_slider = GetComponentInChildren<Slider>();
			_audioSource = GetComponentInChildren<AudioSource>();
			if (_slider && !_audioSource)
				_audioSource = _slider.gameObject.AddComponent<AudioSource>();
		}

		private void OnEnable()
		{
			_slider.onValueChanged.AddListener(OnSliderValueChanged);
		}

		private void OnDisable()
		{
			_slider.onValueChanged.RemoveListener(OnSliderValueChanged);
		}

		private void Update()
		{
			if (_valueChanged)
			{
				HandleValueChanged();
				return;
			}

			HandleStopAudio();
		}

		private void HandleValueChanged()
		{
			_valueChanged = false;
			_timeElapsed = 0;

			if (_audioSource.isPlaying)
			{
				_audioSource.volume = GetVolume();
				return;
			}

			_audioSource.clip = _audioData.AudioClip;
			_audioSource.volume = GetVolume();
			_audioSource.loop = true;
			_audioSource.Play();
		}

		private void HandleStopAudio()
		{
			const float TimeToElapse = .05f;
			if (_timeElapsed > TimeToElapse)
			{
				_audioSource.Stop();
				return;
			}
			_timeElapsed += Time.deltaTime;
		}

		protected virtual void OnSliderValueChanged(float _)
		{
			_valueChanged = true;
		}

		private float GetVolume()
		{
			return _volume * (_ignoreMuted ? AudioSystem.GetVolumeIgnoreMuted(_audioData.SoundType) : AudioSystem.GetVolume(_audioData.SoundType));
		}
	}
}
