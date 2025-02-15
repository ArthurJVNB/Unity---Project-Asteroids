using UnityEngine;

namespace Project
{
	public class Shooter : MonoBehaviour
	{
		[SerializeField] private Transform _positionToShoot;
		[SerializeField] private Bullet _prefab;

		private void Reset()
		{
			_positionToShoot = GetComponentInChildren<Transform>();
		}

		public void OnAttack()
		{
			Shoot();
		}

		private void Shoot()
		{
			var item = Instantiate(_prefab, _positionToShoot.position, _positionToShoot.rotation);
			item.Project(_positionToShoot.up);
		}
	}
}
