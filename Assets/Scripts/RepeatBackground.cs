using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour {

	private BoxCollider2D bc2D;
	private float horizontalLength;
	// Use this for initialization
	void Start () {
		bc2D = GetComponent<BoxCollider2D> ();
		horizontalLength = bc2D.size.x;
	}

	
	// Update is called once per frame
	void Update () {
		if (GameControl.instance.gameOver == false) {
			if (transform.position.x <= -horizontalLength) {
				transform.position = (Vector2)transform.position + new Vector2(2 * horizontalLength,0);
			}
		}
	}
}
