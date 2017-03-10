using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSetter : MonoBehaviour {
	public Sprite[] sprites;
	private Image mImage;
	public void setImage(string skillname){
		Sprite sp= sprites[4]; 
		for(int i = 0 ;i<sprites.Length;i++){
			if (sprites [i].name == skillname) {
				sp =sprites [i];
				break;
			}
		}
		mImage.overrideSprite = sp;
	}
	// Use this for initialization
	void Start () {
		mImage = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
