using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameControl : MonoBehaviour {


	public int gamelevel = 0;
	public bool gameOver = false;
	public bool gameStart = false;
	public GameObject GamOverText;
	public GameObject bird;
	public static GameControl instance;
	public Button StartButton;
	public AudioClip ScoreAudio;
	public AudioClip DeadAudio;
	public AudioClip StartMusic;
	public int pointPerScore = 1;
	public float scrollspeed = -1.5f;
	public Text ScoreText;

	public AudioSource audioplayer;
	private int score = 0 ;
	// Use this for initialization
	void Awake () {
		//If we don't currently have a game control...
		if (instance == null)
			//...set this one to be it...
			instance = this;
		//...otherwise...
		else if(instance != this)
			//...destroy this one because it is a duplicate.
			Destroy (gameObject);
	
	}
	void Start(){
		audioplayer = GetComponent<AudioSource> ();
		StartButton.onClick.AddListener (startBtnClicked);
	}
	void startBtnClicked(){
		gameStart = true;
		StartButton.gameObject.SetActive (false);
		audioplayer.PlayOneShot (StartMusic, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (gameStart == false) {
			return;
		}
		if (gameOver && Input.GetMouseButtonDown (0)) {
			//...reload the current scene.
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}
	public void BirdDied()
	{ 
		if(gameOver==false){
			//Set the game to be over.
			gameOver = true;
		bird.GetComponent<Skill> ().StopAllSkillEffect();
		//Activate the game over text.
		audioplayer.PlayOneShot(DeadAudio,1f);
		GamOverText.SetActive (true);
		
		}
	}

	public void BirdScored(){
		if (gameOver == true) {
			return;
		}
		audioplayer.PlayOneShot (ScoreAudio,1f);
		score +=pointPerScore;
		ScoreText.text = "score : " + score.ToString ();
	}
}
