using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	Rigidbody2D rigi;
	Collider2D Coll;
	// Use this for initialization
	void Start () {
		rigi = GetComponent<Rigidbody2D> ();
		print ("Hola we");
	}
	
	// Update is called once per frame
	void Update () {
		rigi.velocity = 10 * -transform.right;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.gameObject.CompareTag ("Enemy") && !other.gameObject.CompareTag ("Bullet")) {
			Destroy (gameObject);
		}
		if (other.gameObject.CompareTag ("Player")) {
			Destroy (gameObject);
			other.SendMessage ("RestarVida", SendMessageOptions.DontRequireReceiver);
		}
	}
}
