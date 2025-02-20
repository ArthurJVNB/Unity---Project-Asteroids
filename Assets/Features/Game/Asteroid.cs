using UnityEngine;

namespace Project
{
	public class Asteroid : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private Rigidbody2D _rigidbody;
		[Space]
		[SerializeField] private Sprite[] _sprites;
		[SerializeField] private float _size = 1;
		[SerializeField] private float _minSize = 0.5f;
		[SerializeField] private float _maxSize = 1.5f;

		private void Reset()
		{
			_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
			_rigidbody = GetComponentInChildren<Rigidbody2D>();
		}

		private void Start()
		{
			_spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];

			transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
			transform.localScale = Vector3.one * _size;

			_rigidbody.mass = _size;
		}
	}
}
