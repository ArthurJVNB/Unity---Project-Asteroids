using UnityEngine;

namespace Project
{
	public class Bullet : MonoBehaviour
	{
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private float _speed = 500;
		[Min(0)]
		[SerializeField] private float _lifetime = 10;

		private void Reset()
		{
			_rigidbody = GetComponentInChildren<Rigidbody2D>();
		}

		public void Project(Vector2 direction)
		{
			_rigidbody.AddForce(direction * _speed);
			Destroy(gameObject, _lifetime);
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			Destroy(gameObject);
		}
	}
}
