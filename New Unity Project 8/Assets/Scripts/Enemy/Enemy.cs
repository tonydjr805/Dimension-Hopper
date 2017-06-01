using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	[SerializeField]
	private GameObject Bala;
	[SerializeField]
	private GameObject PlayerTarget;
	[SerializeField]
	private LayerMask Capas;
	[SerializeField]
	private float TiempoDeRecarga;
	[SerializeField]
	private bool DireccionInversa;

	Rigidbody2D rigi;

	RaycastHit2D hit;

	bool Shooting;

	int Vida = 100;

	[SerializeField]
	float Distancia = 10;

	bool Recargando;
	// Use this for initialization
	void Start () {
		//Anim = GetComponent<Animator> ();
		rigi = GetComponent<Rigidbody2D> ();
		Shooting = false;
		//StartCoroutine (LanzarBala ());
		//Estado = 1;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 Dir = PlayerTarget.transform.position - transform.position;
		Dir.y = 0;
		
		hit = Physics2D.Raycast (transform.position, -transform.right, Distancia, Capas);
		//print (Dir);
		if (Dir.x > 0) {
			transform.rotation = Quaternion.Euler (0, 180, 0);
		}
		if (Dir.x < 0) {
			transform.rotation = Quaternion.Euler (0, 0, 0);
		}

		//Debug.DrawRay (transform.position, new Vector2 (hit.point.x - transform.position.x, 0));
		if (hit.collider) {
			if (hit.collider.gameObject.CompareTag("Player")) {
				StartCoroutine (LanzarBala ());
			}
		}
	}

	IEnumerator LanzarBala()
	{
		//AnimInfo = Anim.GetCurrentAnimatorStateInfo (0);

		if (!Shooting) {
			//Anim.SetTrigger ("Attack");
			Instantiate (Bala, transform.FindChild ("Instaciate bullet").transform.position, transform.rotation);
			Shooting = true;
			yield return new WaitForSeconds (TiempoDeRecarga);
			Shooting = false;
			//Estado = 0;
		}
	}
	public void BajarVida()
	{
		print (Vida);
		Vida -= 20;
		if (Vida < 1)
			Destroy (gameObject);
	}
}
