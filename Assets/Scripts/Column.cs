using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour {
	public GameObject DestructibleColumn;
	private GameObject UpCol;
	private GameObject DownCol;
	private Vector2 UpCol_pos = new Vector2(0f,6.9f);
	private Vector2 DownCol_pos = new Vector2(0f,-6.9f);
	private Quaternion UpCol_rot = new Quaternion (0, 0, 180, 0);
	private Quaternion DownCol_rot = new Quaternion (0, 0, 0, 0);
	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<BirdController> () != null&& other.GetType()==typeof(PolygonCollider2D)){
			GameControl.instance.BirdScored ();
		}
	}
	// Use this for initialization
	void Start () {
		UpCol = transform.Find("UpColumn").gameObject;
		DownCol = transform.Find ("DownColumn").gameObject;
	}
	void AutoKeepComplete(){
		if (UpCol== null) {
			UpCol= Instantiate (DestructibleColumn,UpCol_pos,UpCol_rot,transform);
			UpCol.transform.localPosition = UpCol_pos;
			UpCol.name="UpColumn";
		} 
		if (transform.Find ("DownColumn") == null) {
			DownCol = Instantiate (DestructibleColumn,DownCol_pos,DownCol_rot,transform);
			DownCol.transform.localPosition = DownCol_pos;
			DownCol.name="DownColumn";
		} 
	}
	// Update is called once per frame
	void Update () {
		if (UpCol== null || DownCol== null) {
			AutoKeepComplete ();
		}
	}
}
