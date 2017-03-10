using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitLifeControl : MonoBehaviour {
	public float lifetime = 3f;
	public static float SpwanX = 15f;
	public static float SpwanY = -10f;
	public AudioClip music;

	private ParticleSystem EatenEffect;
	private bool Triggered = false;
	private string skillname = "";
	private float timecount = 0f;
	private bool isDead = false;
	private float horizontallength = 0f;
	private Color fullColor = new Color(1f,1f,1f,1f);
	private Color fadeColor = new Color(1f,1f,1f,0f);
	private SpriteRenderer spriteRender;
	// Use this for initialization
	void Start () {
		horizontallength = GameObject.Find ("Grass").GetComponent<BoxCollider2D>().size.x;
		skillname = GetComponent<SpriteRenderer> ().sprite.name;
		EatenEffect = GetComponent<ParticleSystem> ();
		spriteRender = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.instance.gameOver == false) {
			if (timecount <= lifetime) {
				timecount += Time.deltaTime;
			} else {
				isDead = true;
			}
			if (isDead == true) {
				if (transform.position.x >= -horizontallength) {// still in the scene
					//leave the scene with a flash
					EatenEffect.Play(true);
					spriteRender.color = Color.Lerp (fullColor, fadeColor, 1.5f);
				}
			}
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Skill> () != null && Triggered == false) {
			other.gameObject.GetComponent<Skill> ().skillTake (skillname);
			EatenEffect.Play(true);
			spriteRender.color = Color.Lerp (fullColor, fadeColor, 2f);
			GameControl.instance.audioplayer.PlayOneShot (music);
			Triggered = true;
		}
	}
	public void Reset(){
		spriteRender.color = fullColor;
		timecount = 0f;
		Triggered = false;
		isDead = false;
	}
}
