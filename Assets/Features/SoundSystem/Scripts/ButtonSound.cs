using UnityEngine;
using UnityEngine.UI;

namespace Project.Sound
{
	public class ButtonSound : MonoBehaviour
	{
		[SerializeField] private AudioData _audioData;
		[Range(0, 1)]
		[SerializeField] private float _volume = 1;
		[Tooltip("If true, the sound will play even if the sound type is muted.")]
		[SerializeField] private bool _ignoreMuted;
		[Space]
		[SerializeField] private Button _button;
		[Tooltip("Optional.\n\nIf not set, it will spawn a GameObject to play as an one shot audio and destroy it. This behaviour is useful when the button is disabled when clicked.")]
		[SerializeField] private AudioSource _audioSource;

		private void Reset()
		{
			_button = GetComponentInChildren<Button>();
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(OnClick);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnClick);
		}

		private void OnClick()
		{
			PlaySound();
		}

		private void PlaySound()
		{
			if (_audioSource)
			{
				_audioSource.clip = _audioData.AudioClip;
				_audioSource.volume = _volume;
				_audioSource.Play();
				return;
			}

			AudioSystem.PlaySound(_audioData, _volume, _ignoreMuted);
		}
	}
}
