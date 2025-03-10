using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{
	public class SceneLoad : MonoBehaviour
	{
		[SerializeField] private string _sceneName;

		public void LoadScene()
		{
			SceneManager.LoadScene(_sceneName);
		}
	}
}
