using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	//Animator anim;
	Rigidbody2D rb;
	public float speed;
	public float jumpForce;
	public float maxSpeed;
	public LayerMask ground;
	bool grounded;
	bool leftSide;
	bool rightSide;
	bool canMove = true;

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


		if (Input.GetKeyDown (KeyCode.Space) && grounded)
			//rb.AddForce (new Vector2 (0, jumpForce)); /another way
			rb.velocity = new Vector2(rb.velocity.x,jumpForce);
		else if (Input.GetKeyDown (KeyCode.Space) && leftSide) 
		{
			rb.velocity = new Vector2 (8, 8);
			StartCoroutine(DisableMove (0.7f));
		} 
		else if (Input.GetKeyDown (KeyCode.Space) && rightSide) 
		{
			rb.velocity = new Vector2 (-8, 8);
			StartCoroutine(DisableMove (0.7f));
		}
	}

	IEnumerator DisableMove(float time)
	{
		canMove = false;
		yield return new WaitForSeconds (time);
		canMove = true;
	}
}
