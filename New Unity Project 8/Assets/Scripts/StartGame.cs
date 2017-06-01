using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
	[SerializeField] private GameObject[] ObjetosMenu;

	void Start()
	{
		Time.timeScale = 1;
		for (int i = 0; i < ObjetosMenu.Length; i++) {
			ObjetosMenu [i].SetActive (false);
		}
	}

	public void Cambio()
	{
		SceneManager.LoadScene (1);
	}
	public void Quit()
	{
		SceneManager.LoadScene (0);
	}
	public void CambioBoss()
	{
		SceneManager.LoadScene (2);
	}
	void Murio()
	{
		Time.timeScale = 0;
		for (int i = 0; i < ObjetosMenu.Length; i++) {
			ObjetosMenu [i].SetActive (true);
		}
	}
}
