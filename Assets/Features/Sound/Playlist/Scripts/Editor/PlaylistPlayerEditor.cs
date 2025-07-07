using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Project.Sound.Playlist.Editor
{
	[CustomEditor(typeof(PlaylistPlayer), true)]
	public class PlaylistPlayerEditor : UnityEditor.Editor
	{
		private static bool _foldoutCurrentAudioDatas = true;
		private PlaylistPlayer Script => (PlaylistPlayer)target;

		public override void OnInspectorGUI()
		{
			Repaint();
			base.OnInspectorGUI();
			DrawCurrentState();
			DrawCurrentAudioData();
			DrawCurrentAudioDatas();
			DrawButtons();
		}

		private void DrawCurrentState()
		{
			bool enabled = GUI.enabled;
			GUI.enabled = false;
			EditorGUILayout.EnumFlagsField("Current State", Script.CurrentState);
			GUI.enabled = enabled;
		}

		private void DrawCurrentAudioData()
		{
			bool enabled = GUI.enabled;
			GUI.enabled = false;
			EditorGUILayout.ObjectField("Current Audio Data", Script.CurrentAudioData, typeof(AudioData), false);
			GUI.enabled = enabled;
		}

		private void DrawCurrentAudioDatas()
		{
			const string FieldName = "_currentAudioDatas";
			const string Label = "Current Audio Datas";

			bool enabled = GUI.enabled;
			
			FieldInfo fieldInfo = typeof(PlaylistPlayer).GetField(FieldName, BindingFlags.NonPublic | BindingFlags.Instance);
			var currentAudioDatas = fieldInfo?.GetValue(Script) as List<AudioData>;
			if (currentAudioDatas == null || currentAudioDatas.Count == 0)
				DrawNone();
			else
				DrawList();
			
			GUI.enabled = enabled;

			void DrawNone()
			{
				GUI.enabled = false;
				EditorGUILayout.LabelField(Label, "null");
			}

			void DrawList()
			{
				_foldoutCurrentAudioDatas = EditorGUILayout.BeginFoldoutHeaderGroup(_foldoutCurrentAudioDatas, Label);
				if (_foldoutCurrentAudioDatas)
				{
					GUI.enabled = false;
					EditorGUI.indentLevel++;
					for (int i = 0; i < currentAudioDatas.Count; i++)
					{
						EditorGUILayout.ObjectField("Element " + i, currentAudioDatas[i], typeof(AudioData), false);
					}
					EditorGUI.indentLevel--;
				}
			}
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
