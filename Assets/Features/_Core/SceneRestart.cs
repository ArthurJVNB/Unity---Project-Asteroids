using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{
	public class SceneRestart : MonoBehaviour
	{
		public void RestartScene()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		}
	}
}
