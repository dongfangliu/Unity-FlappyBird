using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpwan : MonoBehaviour {

	public float minYpos = -2f;
	public float maxYpos = 3.5f;
	public int minSpwanScaleOfCol = 1;
	public int maxSpwanScaleOfCol = 5;
	public GameObject[] fruitPrefab;
	public float SpwanXpos = 15f;

	private GameObject[] fruits = {};
	private float TimeSinceLastSpwan = 0f;
	private float SpwanRate = 6f;
	private Vector2 fruit_pool_pos = new Vector2 (15, -25);
	// Use this for initialization
	float RandomYpos(){
		return Random.Range (minYpos, maxYpos);
	}

	float NextspwanRate(){
		return Random.Range (minSpwanScaleOfCol, maxSpwanScaleOfCol)*ColumnSpwan.spwanRate;
	}
	void Start () {
		fruits = new GameObject[fruitPrefab.Length];
		for (int i = 0; i < fruitPrefab.Length; i++) {
			fruits [i] = Instantiate (fruitPrefab [i], fruit_pool_pos, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.instance.gameStart == false) {
			return;
		}
		if (GameControl.instance.gameOver == false) {
			if (TimeSinceLastSpwan < SpwanRate) {
				TimeSinceLastSpwan += Time.deltaTime;
			} else {
				TimeSinceLastSpwan = 0f;
				SpwanRate = NextspwanRate ();
				int fruitChoice = Random.Range (0, 4);
				GameObject curFruit = fruits [fruitChoice];
				curFruit.GetComponent<FruitLifeControl> ().Reset ();
				curFruit.transform.position = new Vector3 (SpwanXpos, RandomYpos (), 0);
			}
		}
	}
}
