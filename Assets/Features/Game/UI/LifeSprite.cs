using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
	public class LifeSprite : MonoBehaviour
	{
		[SerializeField] private Image _image;
		[SerializeField] private BehaviourType _behaviour = BehaviourType.ActivateDeactivateVisual;
		[SerializeField] private ImageSettings _swapSettingsActive;
		[SerializeField] private ImageSettings _swapSettingsInactive;

		private bool _isActive;
		public bool IsActive { get => _isActive; set => SetActive(value); }

		public enum BehaviourType
		{
			ActivateDeactivateVisual,
			SwapSprite,
		}

		[Serializable]
		private class ImageSettings
		{
			public Sprite Sprite;
			public Color Color = Color.white;

			public void Apply(Image image)
			{
				image.sprite = Sprite;
				image.color = Color;
			}
		}

		private void Reset()
		{
			_image = GetComponentInChildren<Image>();
			_swapSettingsActive = new() { Sprite = _image.sprite };
			_swapSettingsInactive = new() { Sprite = _image.sprite };
		}

		public void SetActive(bool active)
		{
			switch (_behaviour)
			{
				case BehaviourType.SwapSprite:
					HandleSwapBehaviour(active);
					break;
				case BehaviourType.ActivateDeactivateVisual:
				default:
					HandleActivateBehaviour(active);
					break;
			}

			_isActive = active;
		}

		private void HandleActivateBehaviour(bool active)
		{
			gameObject.SetActive(active);
		}

		private void HandleSwapBehaviour(bool active)
		{
			var settings = active ? _swapSettingsActive : _swapSettingsInactive;
			settings.Apply(_image);
		}
	}
}
