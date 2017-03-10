using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill : MonoBehaviour {

	public static string[] skills = {"Invisible","ScoreMore","SmallerSize","Hero"};
	public float skillTime = 5f;
	public float backTime = 3f;
	public GameObject alertImage;
	public bool isHeroMode = false;
	public AudioClip HeroMusic;
	public AudioClip CDMusic;
	private AudioSource Maudio;
	// Use this for initialization
	private bool isSkillTaking = false;
	private Image alertFruit;
	private ImageSetter imageSetter;
	private float updateFillSpeed=0f;
	private float TimeTaking = 0f;
	private Color fullColor = new Color(1f,1f,1f,1f);
	private Color fadeColor = new Color(1f,1f,1f,0.4f);
	private Vector3 fullScale= new Vector3 (1f, 1f, 1f);
	private Vector3 halfScale = new Vector3 (0.5f, 0.5f, 1f);
	private float OriginalScrollSpeed=0f;
	private float OriginalColumnRate = 0f;
	void Start(){
		updateFillSpeed = 1f / skillTime;
		Maudio = GetComponent<AudioSource> ();
		alertFruit =alertImage.GetComponent<Image>();
		imageSetter = alertImage.GetComponent<ImageSetter> ();
		OriginalColumnRate = ColumnSpwan.spwanRate;
		OriginalScrollSpeed = GameControl.instance.scrollspeed;
	}
	void Update(){
		if (GameControl.instance.gameOver == true) {
			StopAllSkillEffect ();
		}
		if (isSkillTaking) {
			TimeTaking += Time.deltaTime;
			if (TimeTaking > skillTime ||alertFruit.fillAmount ==0f) {
				isSkillTaking = false;
			}
			alertFruit.fillAmount -= updateFillSpeed*Time.deltaTime;
		} else {
			TimeTaking = 0f;
			alertFruit.fillAmount = 1f;
			imageSetter.setImage ("empty");
		}
	}
	IEnumerator HeroMode ()
	{
		isSkillTaking = true;
		isHeroMode = true;
		GameControl.instance.scrollspeed *= 2f;
		ColumnSpwan.spwanRate /= 2f;
		GameControl.instance.audioplayer.volume = 0.1f;
		if (Maudio.isPlaying == false) {
			Maudio.Play ();
		}
		yield return new WaitForSeconds (skillTime);
		Maudio.Pause ();
		GameControl.instance.audioplayer.volume = 0.8f;
		GameControl.instance.scrollspeed = OriginalScrollSpeed;
		ColumnSpwan.spwanRate =OriginalColumnRate;
		isSkillTaking = false;
		isHeroMode = false;
	}
	IEnumerator BeInvisible(){
		isSkillTaking = true;
		GetComponent<SpriteRenderer> ().color = Color.Lerp (fullColor, fadeColor, backTime);
		GetComponent<PolygonCollider2D> ().isTrigger = true;
		yield return new WaitForSeconds (skillTime);
		GetComponent<SpriteRenderer> ().color = Color.Lerp (fadeColor,fullColor, backTime);
		GetComponent<PolygonCollider2D> ().isTrigger =false;
		isSkillTaking = false;
	}
	IEnumerator ScoreMore(){
		isSkillTaking = true;
		GameControl.instance.pointPerScore++;
		yield return new WaitForSeconds (skillTime);
		GameControl.instance.pointPerScore--;
		isSkillTaking = false;
	}
 	IEnumerator SmallerSize(){
		isSkillTaking = true;
		gameObject.transform.localScale=Vector3.Lerp(fullScale,halfScale,backTime);
		yield return new WaitForSeconds (skillTime);
		gameObject.transform.localScale=Vector3.Lerp(halfScale,fullScale,backTime);
		isSkillTaking = false;
	}
	IEnumerator PlayCDmusic(){
		yield return new WaitForSeconds (skillTime - 3f);
		Maudio.PlayOneShot (CDMusic);
	}
	public void skillTake(string skillname){
		imageSetter.setImage (skillname);
		StopAllSkillEffect ();
		StartCoroutine (PlayCDmusic ());
		switch (skillname) {
		case "Invisible":
			StartCoroutine (BeInvisible());
			break;
		case "ScoreMore":
			StartCoroutine (ScoreMore());
			break;
		case "SmallerSize":
			StartCoroutine (SmallerSize());
			break;
		case "Hero":
			StartCoroutine (HeroMode ());
			break;
		}

	}
	public void StopAllSkillEffect(){
		StopAllCoroutines ();
		alertFruit.fillAmount = 1f;
		GetComponent<SpriteRenderer> ().color = fullColor;
		GetComponent<PolygonCollider2D> ().isTrigger =false;
		gameObject.transform.localScale = fullScale;
		Maudio.Pause ();
		GameControl.instance.audioplayer.volume = 0.8f;
		GameControl.instance.scrollspeed = OriginalScrollSpeed;
		ColumnSpwan.spwanRate =OriginalColumnRate;
		isSkillTaking = false;
		isHeroMode = false;
	}
}
