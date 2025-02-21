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
		[SerializeField] private float _graceTimeAfterSpawn = 3;

		[Header("Events")]
		[SerializeField] private SpaceshipEventData _spaceshipDiedEvent;
		[SerializeField] private SpaceshipEventData _spaceshipGraceTimeChangedEvent;

		private float _previousThrust;
		[ShowNonSerializedField]
		private float _thrust;

		private float _previousTurnDirection;
		[ShowNonSerializedField]
		private float _turnDirection;

		[ShowNonSerializedField]
		private bool _isGraceTime;

		public bool IsGraceTime => _isGraceTime;

		private void Reset()
		{
			_rigidbody = GetComponentInChildren<Rigidbody2D>();
		}

		private void FixedUpdate()
		{
			if (_thrust != 0)
				_rigidbody.AddForce(_thrustSpeed * _thrust * transform.up);

			if (_turnDirection != 0)
				_rigidbody.AddTorque(_turnDirection * _turnSpeed);
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (_isGraceTime) return;
			if (!collision.gameObject.TryGetComponent(out Asteroid _)) return;
			_spaceshipDiedEvent.Invoke(this);
		}

		public void Enable()
		{
			gameObject.SetActive(true);
		}

		public void Disable()
		{
			_thrust = 0;
			_turnDirection = 0;
			_rigidbody.linearVelocity = Vector2.zero;
			_rigidbody.angularVelocity = 0;
			gameObject.SetActive(false);
		}

		public void OnMove(InputValue value)
		{
			_thrust = value.Get<Vector2>().y switch
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

			if (_previousThrust != _thrust)
				Notify_OnChangedThrust();

			if (_previousTurnDirection != _turnDirection)
				Notify_OnChangedTurnDirection();

			_previousThrust = _thrust;
			_previousTurnDirection = _turnDirection;
		}

		public void StartGraceTimeAfterSpawn()
		{
			StartGraceTime(_graceTimeAfterSpawn);
		}

		public void StartGraceTime(float time)
		{
			Debug.Log($"Start grace time ({time} seconds)");
			_isGraceTime = true;
			_spaceshipGraceTimeChangedEvent.Invoke(this);
			Invoke(nameof(EndGraceTime), time);
		}

		private void EndGraceTime()
		{
			Debug.Log("End grace time");
			_isGraceTime = false;
			_spaceshipGraceTimeChangedEvent.Invoke(this);
		}

		private void Notify_OnChangedThrust()
		{
			OnChangedThrust?.Invoke(_thrust);
		}

		private void Notify_OnChangedTurnDirection()
		{
			OnChangedTurnDirection?.Invoke(_turnDirection);
		}

	}
}
