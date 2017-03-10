using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSpwan : MonoBehaviour {

	public GameObject columnPrefab;
	public int colnums = 5;
	public float min_ypos =-1f;
	public float max_ypos = 3f;
	public float minTriggerYsize =2.8f;
	public float maxTriggerYsize =5.2f;
	public static float spwanRate = 4f;

	private GameObject[] columns;
	private float ColumnHalfHeight = 5.17f;
	private float spwanXpos = 10f;
	private float timeSinceLastSpwan = 0f;
	private int currentCol = 0;

	// Use this for initialization

	void Start () {
		columns = new GameObject[colnums];
		for (int i = 0; i < colnums; i++) {
			columns[i] = (GameObject)Instantiate(columnPrefab,new Vector2 (-15+i*10, -25), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.instance.gameStart == false) {
			return;
		}
		timeSinceLastSpwan += Time.deltaTime;
		if (GameControl.instance.gameOver == false && timeSinceLastSpwan >= spwanRate) {
			timeSinceLastSpwan = 0f;
			SetRandomGap ();
			SetRandomPos ();
			currentCol = (currentCol+1)%colnums;
		}
	}
	void SetRandomPos(){
		float spwanYpos = Random.Range (min_ypos, max_ypos);
		columns [currentCol].transform.position = new Vector2 (spwanXpos, spwanYpos);
		GameObject Down = columns [currentCol].transform.Find ("DownColumn").gameObject;
		GameObject Up = columns [currentCol].transform.Find ("UpColumn").gameObject;
		Down.SetActive (true);
		Up.SetActive (true);
		if (Up.transform.position.y> 10.5f) {
			Up.SetActive (false);
		}
		if (Down.transform.position.y < -7.35f) {
			Down.SetActive (false);
		}
	}
	void SetRandomGap(){
		float triggerYsize = Random.Range (minTriggerYsize, maxTriggerYsize);
		columns [currentCol].GetComponent<BoxCollider2D> ().size= new Vector2(1,triggerYsize+ColumnHalfHeight*2);
		columns [currentCol].transform.Find ("DownColumn").gameObject.transform.localPosition = new Vector2 (0f, -(triggerYsize / 2 + ColumnHalfHeight));
		columns [currentCol].transform.Find ("UpColumn").gameObject.transform.localPosition = new Vector2 (0f, (triggerYsize / 2 + ColumnHalfHeight));
	}
}
