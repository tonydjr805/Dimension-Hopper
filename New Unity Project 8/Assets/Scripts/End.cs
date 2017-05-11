using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour 
{
	GameObject flag;

	// Use this for initialization
	void Start ()
	{
		flag = transform.Find ("Triangle").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
			StartCoroutine (LoadNextLevel());
	}

	IEnumerator LoadNextLevel()
	{
		flag.GetComponent<SpriteRenderer> ().color = new Color (0, .6f, 0);
		yield return new WaitForSeconds (2);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex +1);
	}
}
