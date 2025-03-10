using UnityEngine;

namespace Project
{
	public class ApplicationQuit : MonoBehaviour
	{
		public void Quit()
		{
			Application.Quit();
#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
#endif
		}
	}
}
