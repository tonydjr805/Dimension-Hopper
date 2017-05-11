using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	//[Range(0, 2)]
	//[SerializeField]
	private int Estado;
	//[Range(0, 2)]
	//[SerializeField]
	private int EstadoSiguiente;
	[SerializeField]
	private GameObject Bala;
	[SerializeField]
	private GameObject PlayerTarget;
	[SerializeField]
	private LayerMask Capas;
	[SerializeField]
	private float TiempoDeRecarga;
	private float TiempoRecargaPriv;

	//AnimatorStateInfo AnimInfo;
	//Animator Anim;

	Rigidbody2D rigi;

	RaycastHit2D hit;

	int BalaLanzadas;

	[SerializeField]
	float Distancia = 10;

	bool PlayerVisto;
	bool Recargando;
	// Use this for initialization
	void Start () {
		//Anim = GetComponent<Animator> ();
		rigi = GetComponent<Rigidbody2D> ();
		//StartCoroutine (LanzarBala ());
		//Estado = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Estado == 0)
			//Anim.SetBool ("Run", false);
		if (Estado == 1)
			//Anim.SetBool ("Run", true);
		if (Estado == 2)
			StartCoroutine (LanzarBala ());
		Vector2 Dir = PlayerTarget.transform.position - transform.position;
		Dir.y = 0;
		hit = Physics2D.Raycast (transform.position, transform.right, Distancia, Capas);

		if (Dir.x < 0 && !Recargando) {
			transform.rotation = Quaternion.Euler (0, 180, 0);
		}
		if (Dir.x > 0 && !Recargando) {
			transform.rotation = Quaternion.Euler (0, 0, 0);
		}

		//Debug.DrawRay (transform.position, new Vector2 (hit.point.x - transform.position.x, 0));
		if (hit.collider) {
			if (hit.collider.gameObject.name == "Player") {
				PlayerVisto = true;
				if (!Recargando) {
					Estado = 2;
				}
			}
		}

		if (Recargando) {
			Recargar ();
			Huir (Dir);
		}
		if(!Recargando && Estado != 2 && PlayerVisto){
			Move (Dir.normalized);
		}
		
		Debug.DrawRay (transform.position, transform.right * Distancia);
	}

	IEnumerator LanzarBala()
	{
		//AnimInfo = Anim.GetCurrentAnimatorStateInfo (0);

		if (BalaLanzadas == 0) {
			//Anim.SetTrigger ("Attack");
			BalaLanzadas++;
			Estado = 0;
			yield return new WaitForSeconds (0.25f);
			Instantiate (Bala, transform.FindChild ("Instaciate bullet").transform.position, transform.rotation);
			yield return new WaitForSeconds (0.55f);
			Recargando = true;
		}
	}
	void Recargar()
	{
		TiempoRecargaPriv += 1 * Time.deltaTime;
		if (TiempoRecargaPriv >= TiempoDeRecarga) {
			TiempoRecargaPriv = 0;
			Recargando = false;
			BalaLanzadas = 0;
		}
	}
	void Move(Vector2 Direccion)
	{
		if (Vector3.Distance (transform.position, PlayerTarget.transform.position) > Distancia) {
			rigi.velocity = Direccion * 2;
			Estado = 1;
		}
	}
	void Huir(Vector2 Direccion)
	{

		if (Vector2.Distance(transform.position, hit.point) > 2) {
			rigi.velocity = -Direccion.normalized * 2;
			Estado = 1;
			if (Direccion.x > 0) {
				transform.rotation = Quaternion.Euler (0, 180, 0);
			}
			if (Direccion.x < 0) {
				transform.rotation = Quaternion.Euler (0, 0, 0);
			}
		}
		else
			Estado = 0;
	}
}
