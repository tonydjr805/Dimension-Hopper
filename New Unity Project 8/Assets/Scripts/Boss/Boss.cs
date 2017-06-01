using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss : MonoBehaviour {
	[SerializeField] private GameObject Bullet;
	[SerializeField] private Transform PosBullet;
	[SerializeField] private float TimeRespanw;
	[SerializeField] private GameObject Head;
	[SerializeField] private Scrollbar UIVida;

	float TimeFire;
	float TimeDificul;

	[SerializeField] float Vida = 100;
	int NBalls;

	bool Dead;

	Animator anim;
	AnimatorStateInfo InfoAnim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		TimeDificul = 6;
	}
	
	// Update is called once per frame
	void Update () {
		if (!Dead) {
			InfoAnim = anim.GetCurrentAnimatorStateInfo (0);
			TimeFire += 1 * Time.deltaTime;
			if (TimeFire > TimeDificul) {
				anim.SetTrigger ("Attack");
				TimeFire = 0;
			}
			if (Vida > 40 && Vida < 70) {
				TimeDificul = 5;
				TimeRespanw = 0.2f;
			}
			if (Vida > 10 && Vida < 20) {
				TimeDificul = 4;
				TimeRespanw = 0.1f;
			}
			if (Vida > 0 && Vida < 10) {
				TimeDificul = 3;
				TimeRespanw = 0.1f;
			}

			if (InfoAnim.IsName ("Attack")) {
				StartCoroutine (Fire ());
			}
			if (Vida < 0.01f) {
				Dead = true;
				anim.SetBool ("Dead", true);
			}
			UIVida.size = Vida / 100;
		}
	}
	IEnumerator Fire()
	{
		if (NBalls == 0) {
			Instantiate (Bullet, PosBullet.position, PosBullet.rotation);
			NBalls++;
			yield return new WaitForSeconds (TimeRespanw);
			NBalls = 0;
		}
	}
	public void Damage()
	{
		Vida -= 0.5f;
		StartCoroutine (ChangeColor ());
	}
	IEnumerator ChangeColor()
	{
		Head.GetComponent<SpriteRenderer> ().color = Color.red;
		yield return new WaitForSeconds (0.01f);
		Head.GetComponent<SpriteRenderer> ().color = Color.white;
	}
}
