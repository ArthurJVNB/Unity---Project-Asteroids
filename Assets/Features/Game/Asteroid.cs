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
		[SerializeField] private float _speed = 50;
		[Min(1)]
		[SerializeField] private float _lifetime = 30;

		public float Size { get => _size; set => _size = value; }
		public float MinSize => _minSize;
		public float MaxSize => _maxSize;
		public float Speed { get => _speed; set => _speed = value; }

		private void Reset()
		{
			_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
			_rigidbody = GetComponentInChildren<Rigidbody2D>();
		}

		private void Start()
		{
			if (_sprites?.Length > 0)
				_spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];

			transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
			transform.localScale = Vector3.one * _size;

			_rigidbody.mass = _size;
			Destroy(gameObject, _lifetime);
		}

		public void SetTrajectory(Vector2 direction)
		{
			_rigidbody.AddForce(direction * _speed);
		}
	}
}
