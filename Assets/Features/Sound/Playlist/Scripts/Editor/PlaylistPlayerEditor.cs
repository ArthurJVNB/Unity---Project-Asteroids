using UnityEditor;
using UnityEngine;

namespace Project.Sound.Playlist.Editor
{
	[CustomEditor(typeof(PlaylistPlayer))]
	public class PlaylistPlayerEditor : UnityEditor.Editor
	{
		private PlaylistPlayer Script => (PlaylistPlayer)target;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			DrawCurrentAudioData();
			DrawButtons();
		}

		private void DrawCurrentAudioData()
		{
			bool enabled = GUI.enabled;
			GUI.enabled = false;
			EditorGUILayout.ObjectField("Current Audio Data", ((PlaylistPlayer)target).CurrentAudioData, typeof(AudioData), false);
			GUI.enabled = enabled;
		}

		private void DrawButtons()
		{
			EditorGUILayout.Space();

			if (!Application.isPlaying)
			{
				EditorGUILayout.HelpBox("If you enter Play Mode, buttons to command the playlist will appear here.", MessageType.Info);
				return;
			}

			if (GUILayout.Button("▶ Play"))
				Script.Play();

			if (GUILayout.Button("▮▮ Pause"))
				Script.Pause();

			if (GUILayout.Button("◼ Stop"))
				Script.Stop();

			if (GUILayout.Button("∞ Shuffle"))
				Script.Shuffle();

			if (GUILayout.Button("▷▷ Next"))
				Script.Next();

			if (GUILayout.Button("◁◁ Previous"))
				Script.Previous();

			if (GUILayout.Button("▶▶ Play Next"))
				Script.PlayNext();

			if (GUILayout.Button("◀◀ Play Previous"))
				Script.PlayPrevious();
		}
	}
}
