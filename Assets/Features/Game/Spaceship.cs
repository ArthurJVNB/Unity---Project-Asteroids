using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project
{
	public class Spaceship : MonoBehaviour
	{
		public event Action<float> OnChangedThrust;
		public event Action<float> OnChangedTurnDirection;

		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private float _thrustSpeed = 1;
		[SerializeField] private float _turnSpeed = 1;
		[SerializeField] private SpaceshipEventData _spaceshipDiedEvent;

		private float _previousThrust;
		[ShowNonSerializedField]
		private float _thurst;

		private float _previousTurnDirection;
		[ShowNonSerializedField]
		private float _turnDirection;

		private void Reset()
		{
			_rigidbody = GetComponentInChildren<Rigidbody2D>();
		}

		private void FixedUpdate()
		{
			if (_thurst != 0)
				_rigidbody.AddForce(_thrustSpeed * _thurst * transform.up);

			if (_turnDirection != 0)
				_rigidbody.AddTorque(_turnDirection * _turnSpeed);
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (!collision.gameObject.TryGetComponent(out Asteroid _)) return;
			_spaceshipDiedEvent.Invoke(this);
		}

		public void OnMove(InputValue value)
		{
			_thurst = value.Get<Vector2>().y switch
			{
				> 0 => 1,
				< 0 => -1,
				_ => 0,
			};
			_turnDirection = value.Get<Vector2>().x switch
			{
				> 0 => -1,
				< 0 => 1,
				_ => 0,
			};

			if (_previousThrust != _thurst)
				Notify_OnChangedThrust();

			if (_previousTurnDirection != _turnDirection)
				Notify_OnChangedTurnDirection();

			_previousThrust = _thurst;
			_previousTurnDirection = _turnDirection;
		}

		private void Notify_OnChangedThrust()
		{
			OnChangedThrust?.Invoke(_thurst);
		}

		private void Notify_OnChangedTurnDirection()
		{
			OnChangedTurnDirection?.Invoke(_turnDirection);
		}

	}
}
