using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {
	public float upForce = 200f;
	private Skill BirdSkill;
	private bool isDead = false;
	private Rigidbody2D rb2d;
	private Animator anim;
	// Use this for initialization
	void Start () {
		BirdSkill = GetComponent<Skill> ();
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.instance.gameStart == false) {
			anim.SetTrigger ("Flap");
		}else if (isDead == false) {
			rb2d.bodyType = RigidbodyType2D.Dynamic;
			if (Input.GetMouseButton (0)) {
				rb2d.velocity = Vector2.zero;
				rb2d.AddForce (new Vector2 (0, upForce));
				anim.SetTrigger ("Flap");
			}
			if (gameObject.transform.position.y <= -2.169845||gameObject.transform.position.y >= 4.72) {
				isDead = true;
				anim.SetTrigger ("Die");
				GameControl.instance.BirdDied ();
			}

		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (BirdSkill.isHeroMode == false) {
			isDead = true;
			rb2d.velocity = Vector2.zero;
			anim.SetTrigger ("Die");
			GameControl.instance.BirdDied ();
		}
	}
}
