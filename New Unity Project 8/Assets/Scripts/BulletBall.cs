using UnityEngine;
using System.Collections;

public class BulletBall : MonoBehaviour {
	Rigidbody2D Rigi;
	// Use this for initialization
	void Start () {
		Rigi = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Rigi.velocity = transform.right * 10;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Enemy")) {
			other.transform.SendMessage ("BajarVida", SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}
		if (!other.gameObject.CompareTag ("Player") && !other.gameObject.CompareTag ("Enemy")) {
			Destroy (gameObject);
			if (other.gameObject.CompareTag("EnemyHead"))
				other.transform.parent.SendMessage ("Damage");
		}

		StartCoroutine (DestroyBall ());
	}
	IEnumerator DestroyBall()
	{
		yield return new WaitForSeconds (5);
		Destroy (gameObject);
	}
}
