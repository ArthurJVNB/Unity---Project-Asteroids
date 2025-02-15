using UnityEngine;
using UnityEngine.InputSystem;

namespace Project
{
	public class Spaceship : MonoBehaviour
	{
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private float _thrustSpeed = 1;

		bool _isThrusting;

		private void Reset()
		{
			_rigidbody = GetComponentInChildren<Rigidbody2D>();
		}

		public void OnMove(InputValue value)
		{
			Debug.Log("OnMove " + value.Get<Vector2>());
			_isThrusting = value.Get<Vector2>() != Vector2.zero;
		}

		public void Thrust()
		{
			_isThrusting = true;
		}

		private void FixedUpdate()
		{
			if (_isThrusting)
			{

			}

		}
	}
}
