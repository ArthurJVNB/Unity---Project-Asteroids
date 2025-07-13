using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project
{
	public class LifeUI : MonoBehaviour
	{
		[SerializeField] private RectTransform _content;
		[SerializeField] private LifeSprite _template;
		[Space]
		[SerializeField] private LifeManager _lifeManager;
		[SerializeField] private IntEventData _lifesChangedEvent;

		[Obsolete]
		private List<GameObject> _activeObjectsObsolete;
		private List<LifeSprite> _objects;

		private int ActiveObjectsCount => _objects?.Count(v => v.IsActive) ?? 0;

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
			_objects ??= new();
			if (ActiveObjectsCount == lifes) return;

			int maxLifes = _lifeManager.MaxLifes;
			TryInstantiateObjects(maxLifes);
			for (int i = 0; i < maxLifes; i++)
				_objects[i].IsActive = i < lifes;
		}

		private void TryInstantiateObjects(int count)
		{
			int addObjects = count - _objects.Count;
			for (int i = 0; i < addObjects; i++)
			{
				var item = Instantiate(_template, _content);
				item.gameObject.SetActive(true);
				_objects.Add(item);
			}
		}

		[Obsolete]
		private void UpdateLifeUIObsolete(int lifes)
		{
			_activeObjectsObsolete ??= new();
			if (_activeObjectsObsolete.Count == lifes) return;

			ClearActiveObjectsObsolete();
			for (int i = 0; i < lifes; i++)
			{
				var item = Instantiate(_template, _content);
				item.gameObject.SetActive(true);
				_activeObjectsObsolete.Add(item.gameObject);
			}
		}

		[Obsolete]
		private void ClearActiveObjectsObsolete()
		{
			foreach (var item in _activeObjectsObsolete)
				Destroy(item.gameObject);
			_activeObjectsObsolete.Clear();
		}
	}
}
