using UnityEngine;

namespace Project
{
	public class AsteroidSpawner : MonoBehaviour
	{
		[Min(0)]
		[SerializeField] private float _spawnRate = 2;
		[Min(0)]
		[SerializeField] private int _spawnAmount = 1;
		[Min(.1f)]
		[SerializeField] private float _spawnRadius = 10;
		[SerializeField] private float _trajectoryVarianceDegrees = 15;
		[SerializeField] private Asteroid _prefab;

		private void Start()
		{
			InvokeRepeating(nameof(Spawn), 0, _spawnRate);
		}

		private void Spawn()
		{
			for (int i = 0; i < _spawnAmount; i++)
			{
				Vector2 direction = Random.insideUnitCircle.normalized * _spawnRadius;
				Vector2 position = (Vector2)transform.position + direction;
				Quaternion rotation = Quaternion.AngleAxis(Random.Range(-_trajectoryVarianceDegrees, _trajectoryVarianceDegrees), Vector3.forward);

				var asteroid = Instantiate(_prefab, position, rotation);
				asteroid.Size = Random.Range(asteroid.MinSize, asteroid.MaxSize);
				asteroid.SetTrajectory(rotation * -direction);
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = new Color(1, 0, 0, .1f);
			Gizmos.DrawWireSphere(transform.position, Mathf.Abs(_spawnRadius));
		}
	}
}
