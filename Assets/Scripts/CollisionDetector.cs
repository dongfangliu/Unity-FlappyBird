using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Destructible2D;

public class CollisionDetector : MonoBehaviour {
	public AudioClip stone;
	private D2dDestructible d;
	// Use this for initialization
	void Start () {
		d = GetComponent<D2dDestructible> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D other){
		if (GameControl.instance.gameOver == false) {
			if (other.gameObject.tag == "Bird") {
				gameObject.GetComponent<Rigidbody2D> ().freezeRotation = false;
				d.Indestructible = false;
				d.Damage = 200f;
				GameControl.instance.audioplayer.PlayOneShot (stone,1f);
				if (other.gameObject.GetComponent<Skill> ().isHeroMode == true) {
					gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
					gameObject.GetComponent<D2dDestroyer> ().enabled = true;
				}
			} else if (other.gameObject.tag == "Obstacle" && other.transform.parent== gameObject.transform.parent &&other.gameObject.name == gameObject.name) {// can only be effected by the sprites from self
				gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
				gameObject.GetComponent<D2dDestroyer> ().enabled = true;
			}
		}
	}
}
