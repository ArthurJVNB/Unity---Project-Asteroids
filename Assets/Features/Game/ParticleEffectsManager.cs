using UnityEngine;

namespace Project
{
	public class ParticleEffectsManager : MonoBehaviour
	{
		[SerializeField] private ParticleSystem _spaceshipExplosionEffect;
		[SerializeField] private ParticleSystem _asteroidCollisionBulletEffect;
		[SerializeField] private ParticleSystem _asteroidCollisionAsteroidEffect;
		[SerializeField] private ParticleSystem _asteroidExplosionEffect;

		[Header("Events Listened")]
		[SerializeField] private Collision2DEventData _spaceshipCollidedAsteroidEvent;
		[SerializeField] private Collision2DEventData _asteroidCollidedBulletEvent;
		[SerializeField] private Collision2DEventData _asteroidCollidedAsteroidEvent;
		[SerializeField] private Collision2DEventData _asteroidExplodedEvent;

		private void OnEnable()
		{
			_spaceshipCollidedAsteroidEvent.Event += SpaceshipCollidedAsteroid_Event;
			_asteroidCollidedBulletEvent.Event += AsteroidCollidedBullet_Event;
			_asteroidCollidedAsteroidEvent.Event += AsteroidCollidedAsteroid_Event;
			_asteroidExplodedEvent.Event += AsteroidExploded_Event;
		}

		private void OnDisable()
		{
			_spaceshipCollidedAsteroidEvent.Event -= SpaceshipCollidedAsteroid_Event;
			_asteroidCollidedBulletEvent.Event -= AsteroidCollidedBullet_Event;
			_asteroidCollidedAsteroidEvent.Event -= AsteroidCollidedAsteroid_Event;
			_asteroidExplodedEvent.Event -= AsteroidExploded_Event;
		}

		private void SpaceshipCollidedAsteroid_Event(Collision2D collision)
		{
			Instantiate(_spaceshipExplosionEffect, collision.otherCollider.transform.position, Random.rotation).Play(true);
		}

		private void AsteroidCollidedBullet_Event(Collision2D collision)
		{
			Instantiate(_asteroidCollisionBulletEffect, collision.transform.position, Random.rotation).Play(true);
		}

		private void AsteroidCollidedAsteroid_Event(Collision2D collision)
		{
			if (IsOtherColliderFaster(collision)) return;

			var rotation = Quaternion.AngleAxis(Vector2.Angle(Vector2.up, collision.contacts[0].normal), Vector3.forward);
			var fx = Instantiate(_asteroidCollisionAsteroidEffect, collision.contacts[0].point, rotation);
			fx.transform.localScale *= collision.relativeVelocity.magnitude;
			fx.Play(true);

#if UNITY_EDITOR
			//Debug.DrawLine(contact.point, (contact.point + contact.normal), Color.red, 1);
			Debug.DrawLine(collision.contacts[0].point, collision.contacts[0].point + (Vector2)(rotation * Vector2.one), Color.red, 1);
#endif
		}

		private void AsteroidExploded_Event(Collision2D collision)
		{
			Instantiate(_asteroidExplosionEffect, collision.otherCollider.transform.position, Random.rotation).Play(true);
		}

		private static bool IsOtherColliderFaster(Collision2D collision)
		{
			return collision.collider.attachedRigidbody.linearVelocity.sqrMagnitude < collision.otherCollider.attachedRigidbody.linearVelocity.sqrMagnitude;
		}

	}
}
