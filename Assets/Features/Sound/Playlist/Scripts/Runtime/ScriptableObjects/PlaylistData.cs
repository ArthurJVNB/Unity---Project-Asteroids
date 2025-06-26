using UnityEngine;

namespace Project.Sound.Playlist
{
	[CreateAssetMenu(fileName = "PlaylistData", menuName = "Scriptable Objects/Sound System/Playlist Data")]
	public class PlaylistData : ScriptableObject
	{
		[SerializeField] private AudioData[] _audioDatas;

		//NOTE: TALVEZ ESSE SCRIPTABLE SEJA DELETADO

		public void Play(AudioSource audioSource, bool ignoreMuted = false)
		{

		}
	}
}
