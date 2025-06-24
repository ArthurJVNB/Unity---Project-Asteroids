using UnityEngine;

namespace Project.Sound
{
	public abstract class AAudioData : ScriptableObject
	{
		public abstract void Play(AudioSource audioSource, bool ignoreMuted = false);

		public abstract void PlayOneShot(Vector3 position, bool ignoreMuted = false);
	}
}
