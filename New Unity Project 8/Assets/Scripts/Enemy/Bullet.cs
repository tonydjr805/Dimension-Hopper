using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	Rigidbody2D rigi;
	Collider2D Coll;
	// Use this for initialization
	void Start () {
		rigi = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		rigi.velocity = 10 * transform.right;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name != "Enemy")
		Destroy (gameObject);
	}
}
