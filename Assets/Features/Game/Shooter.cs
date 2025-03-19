using Project.Sound;
using UnityEngine;

namespace Project
{
	public class Shooter : MonoBehaviour
	{
		[SerializeField] private Transform _positionToShoot;
		[SerializeField] private Bullet _prefab;
		[Tooltip("Optional. If set, it will play the audio data on shoot")]
		[SerializeField] private AudioDataArray _shootAudioDataArray;

		[Header("Events Invoked")]
		[SerializeField] private Vector2EventData _onShootEvent;

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
			if (_shootAudioDataArray) _shootAudioDataArray.PlayOneShot(item.transform.position);
			_onShootEvent.Invoke(item.transform.position);
		}
	}
}
