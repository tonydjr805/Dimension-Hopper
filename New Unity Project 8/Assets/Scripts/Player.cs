using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	//Animator anim;
	Rigidbody2D rb;
	public float speed;
	public float jumpForce;
	public float maxSpeed;
	public float SpeedShoot;
	public LayerMask ground;
	public GameObject Bullet;
	bool grounded;
	bool leftSide;
	bool rightSide;
	bool canMove = true;
	int Vida = 100;
	bool Shooting;
	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		//anim = GetComponent<Animator> ();
		//PlayerPrefs.SetInt ("Score", 0);/ to reset the score
	}
	
	// Update is called once per frame
	void Update () 
	{
		grounded = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y - 1), Vector2.down, 0.1f, ground).collider!=null;

		leftSide = Physics2D.Raycast (new Vector2 (transform.position.x-0.7f, transform.position.y), Vector2.down, 0.1f, ground).collider!=null;
		rightSide = Physics2D.Raycast (new Vector2 (transform.position.x+0.7f, transform.position.y), Vector2.down, 0.1f, ground).collider!=null;

		if (rb.velocity.x > -maxSpeed && Input.GetAxis ("Horizontal") < 0&& canMove)
			rb.AddForce (new Vector2 (-speed, 0));
		else if (rb.velocity.x < maxSpeed && Input.GetAxis ("Horizontal") > 0&& canMove)
			rb.AddForce (new Vector2 (speed, 0));

		//anim.SetBool ("Walking", Input.GetAxis ("Horizontal") != 0);
		

		if (Input.GetAxis ("Horizontal") < 0)
			transform.localScale = new Vector3 (-4, 4, 1);
		else if (Input.GetAxis("Horizontal") > 0)
			transform.localScale = new Vector3 (4, 4, 1);


		if (Input.GetKeyDown (KeyCode.W) && grounded)
			//rb.AddForce (new Vector2 (0, jumpForce)); /another way
			rb.velocity = new Vector2(rb.velocity.x,jumpForce);
		else if (Input.GetKeyDown (KeyCode.W) && leftSide) 
		{
			rb.velocity = new Vector2 (8, 8);
			StartCoroutine(DisableMove (0.7f));
		} 
		else if (Input.GetKeyDown (KeyCode.W) && rightSide) 
		{
			rb.velocity = new Vector2 (-8, 8);
			StartCoroutine(DisableMove (0.7f));
		}
		if (Input.GetMouseButtonDown (1))
			StartCoroutine (Disparo ());
	}

	IEnumerator DisableMove(float time)
	{
		canMove = false;
		yield return new WaitForSeconds (time);
		canMove = true;
	}
	IEnumerator Disparo()
	{
		if (!Shooting) {
			Shooting = true;
			Vector2 Diff = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
			Diff.Normalize ();
			float Angle = Mathf.Atan2 (Diff.y, Diff.x) * Mathf.Rad2Deg;
			Vector3 Pos = new Vector3 (transform.position.x, transform.position.y, -5);
			Instantiate (Bullet, Pos, Quaternion.Euler (0, 0, Angle));
			yield return new WaitForSeconds (SpeedShoot);
			Shooting = false;
		}
	}
	void RestarVida()
	{
		Vida -= 5;
		if (Vida < 1) {
			Camera.main.gameObject.SendMessage ("Murio", SendMessageOptions.DontRequireReceiver);
		}
	}
}
