using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (GameControl.instance.gameOver == false){
			Vector3 positionNow = transform.position;
			transform.position = new Vector3 (positionNow.x + GameControl.instance.scrollspeed * Time.deltaTime, positionNow.y, positionNow.z);
		}
	}
}
