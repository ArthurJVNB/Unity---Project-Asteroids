using UnityEngine;

namespace Project
{
	public class Asteroid : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private Sprite[] _sprites;

		private void Reset()
		{
			_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
			_rigidbody = GetComponentInChildren<Rigidbody2D>();
		}


	}
}
