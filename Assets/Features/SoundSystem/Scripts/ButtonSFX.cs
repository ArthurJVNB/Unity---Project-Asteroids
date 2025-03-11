using UnityEngine;
using UnityEngine.UI;

namespace Project.Sound
{
	public class ButtonSFX : MonoBehaviour
	{
		[SerializeField] private Button _button;
		[Space]
		[SerializeField] private AudioData _audioData;
		[Range(0, 1)]
		[SerializeField] private float _volume = 1;

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
			AudioSystem.PlaySound(_audioData, _volume);
		}
	}
}
