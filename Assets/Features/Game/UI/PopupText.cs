using TMPro;
using UnityEngine;

namespace Project
{
	public class PopupText : MonoBehaviour
	{
		private const float DefaultTimeToDestroy = 2;

		[SerializeField] private TMP_Text _text;

		private void Reset()
		{
			_text = GetComponentInChildren<TMP_Text>();
		}

		public static PopupText Create(PopupText prefab, Transform parent, string text, float timeToDestroy = DefaultTimeToDestroy, Vector3? worldPosition = null)
		{
			var popup = Instantiate(prefab, parent);
			popup.Setup(text, timeToDestroy, worldPosition);
			return popup;
		}

		public static PopupText Create(PopupText prefab, string text, float timeToDestroy = DefaultTimeToDestroy, Vector3? worldPosition = null)
		{
			return Create(prefab, null, text, timeToDestroy, worldPosition);
		}

		public void Setup(string text, float timeToDestroy = DefaultTimeToDestroy, Vector3? worldPosition = null)
		{
			SetText(text);
			Destroy(gameObject, timeToDestroy);
			if (worldPosition.HasValue)
				transform.position = worldPosition.Value;
		}

		private void SetText(string text)
		{
			_text.text = text;
		}
	}
}
