using UnityEngine;
using System.Collections;

public class BulletBoss1 : MonoBehaviour {
	[SerializeField] private GameObject Target;
	[SerializeField] private float Velocity;

	Vector3 Dir;

	Rigidbody2D Rigi;
	// Use this for initialization
	void Start () {
		Target = GameObject.FindGameObjectWithTag ("Player");
		Rigi = GetComponent<Rigidbody2D> ();
		Dir = (Target.transform.position - transform.position);
		Dir.Normalize ();
		Vector3 Dir2 = (Target.transform.position - transform.position);
		float RotZ = Mathf.Atan2 (Dir2.x, Dir2.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0, 0, RotZ + 90);
		transform.position = new Vector3 (transform.position.x, transform.position.y, -5);
	}
	
	// Update is called once per frame
	void Update () {
		
		Rigi.velocity = Dir * Velocity;
		print (Dir);
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.gameObject.CompareTag("Bullet") && !other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("EnemyHead"))
			Destroy (gameObject);
		if (other.gameObject.CompareTag ("Player")) {
			Destroy (gameObject);
			other.SendMessage ("RestarVida", SendMessageOptions.DontRequireReceiver);
		}
	}
}
