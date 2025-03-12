using UnityEngine;
using UnityEngine.UI;

namespace Project.Sound
{
	public class ButtonSFX : MonoBehaviour
	{
		[SerializeField] private AudioData _audioData;
		[Range(0, 1)]
		[SerializeField] private float _volume = 1;
		[Tooltip("If true, the sound will play even if the sound type is muted.")]
		[SerializeField] private bool _ignoreMuted;
		[Space]
		[SerializeField] private Button _button;

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
			AudioSystem.PlaySound(_audioData, _volume, _ignoreMuted);
		}
	}
}
