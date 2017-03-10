using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {
	private Skill BirdSkill;
	private ParticleSystem FireFlame;
	private CapsuleCollider Mcollider;
	// Use this for initialization
	void Start () {
		BirdSkill = GetComponentInParent<Skill> ();
		FireFlame = GetComponent<ParticleSystem> ();
		Mcollider = GetComponent<CapsuleCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (FireFlame != null) {
			if (BirdSkill.isHeroMode == true) {
				Mcollider.enabled = true;
				if (FireFlame.isPlaying == false||FireFlame.isPaused==true) {
					FireFlame.Play ();
				}
			} else {
				Mcollider.enabled = false;
				FireFlame.Stop ();
			}
		}
	}
}
