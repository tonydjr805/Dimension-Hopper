using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour 
{
	Vector3 pp;

	// Use this for initialization
	void Start () 
	{
		pp = GameObject.FindGameObjectWithTag ("Player").transform.position;

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
			other.gameObject.gameObject.transform.position = pp;
	}
}
