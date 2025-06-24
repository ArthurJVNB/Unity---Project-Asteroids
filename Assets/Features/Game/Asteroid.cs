using Project.Sound;
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
		[SerializeField] private AAudioData _asteroidCollisionAudioData;
		[SerializeField] private AAudioData _asteroidExplosionAudioData;
		[Header("Events Invoked")]
		[SerializeField] private Collision2DEventData _asteroidCollidedBulletEvent;
		[SerializeField] private Collision2DEventData _asteroidCollidedAsteroidEvent;
		[SerializeField] private Collision2DEventData _asteroidExplodedEvent;
		[SerializeField] private AsteroidEventData _asteroidDestroyedData;

		private bool _enteredGameArea;

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
			Setup();
		}

		private void OnBecameVisible()
		{
			_enteredGameArea = true;
		}

		private void OnBecameInvisible()
		{
			if (_enteredGameArea)
				Destroy(gameObject);
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.TryGetComponent(out Asteroid _))
			{
				_asteroidCollidedAsteroidEvent.Invoke(collision);
				if (_asteroidCollisionAudioData) _asteroidCollisionAudioData.PlayOneShot(transform.position);
				return;
			}

			if (collision.gameObject.TryGetComponent(out Bullet _))
			{
				if (_size > _minSize)
				{
					CreateSplit();
					CreateSplit();
					_asteroidCollidedBulletEvent.Invoke(collision);
				}
				else
				{
					_asteroidExplodedEvent.Invoke(collision);
				}
				_asteroidDestroyedData.Invoke(this);
				if (_asteroidExplosionAudioData) _asteroidExplosionAudioData.PlayOneShot(transform.position);
				Destroy(gameObject);
				return;
			}
		}

		public void SetTrajectory(Vector2 direction)
		{
			_rigidbody.AddForce(direction * _speed);
		}

		private void Setup()
		{
			if (_sprites?.Length > 0)
				_spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];

			transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
			transform.localScale = Vector3.one * _size;

			_rigidbody.mass = _size;

			Invoke(nameof(CheckShouldDestroy), 30);
		}

		private void CheckShouldDestroy()
		{
			if (_enteredGameArea) return;
#if UNITY_EDITOR || DEVELOPMENT_BUILD
			Debug.Log("FORCE DESTROY ASTEROID (after 30 seconds never entered the game area)");
#endif
			Destroy(gameObject);
		}

		private void CreateSplit()
		{
			var asteroid = Instantiate(this, transform.position, transform.rotation);
			asteroid.Size = _size * .5f;
			asteroid.Speed = _speed * 1.5f;
			asteroid.SetTrajectory(Random.insideUnitCircle.normalized);
		}
	}
}
