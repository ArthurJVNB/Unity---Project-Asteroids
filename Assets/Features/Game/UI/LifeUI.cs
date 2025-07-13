using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
	public class LifeUI : MonoBehaviour
	{
		[SerializeField] private RectTransform _content;
		[SerializeField] private RectTransform _template;
		[Space]
		[SerializeField] private LifeManager _lifeManager;
		[SerializeField] private IntEventData _lifesChangedEvent;

		private List<RectTransform> _activeObjects;

		private void OnEnable()
		{
			_lifesChangedEvent.Event += LifesChangedEvent_Event;
			UpdateLifeUI(_lifeManager.Lifes);
		}

		private void OnDisable()
		{
			_lifesChangedEvent.Event -= LifesChangedEvent_Event;
			UpdateLifeUI(0);
		}

		private void LifesChangedEvent_Event(int lifes)
		{
			UpdateLifeUI(lifes);
		}

		private void UpdateLifeUI(int lifes)
		{
			_activeObjects ??= new();
			if (_activeObjects.Count == lifes) return;

			ClearActiveObjects();
			for (int i = 0; i < lifes; i++)
			{
				var item = Instantiate(_template, _content);
				item.gameObject.SetActive(true);
				_activeObjects.Add(item);
			}
		}

		private void ClearActiveObjects()
		{
			foreach (var item in _activeObjects)
				Destroy(item.gameObject);
			_activeObjects.Clear();
		}
	}
}
