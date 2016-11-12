using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiColliderSprite : MonoBehaviour {

	private SpriteRenderer sp;
	public bool parent = false; 

	void Awake(){
		if (!parent) {
			sp = this.GetComponent<SpriteRenderer> ();
		} else {
			sp = this.transform.parent.GetComponent<SpriteRenderer> ();
		}

		foreach (Transform child in this.transform) {
			child.gameObject.SetActive (false);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		string spriteName = sp.sprite.name;

		foreach (Transform child in this.transform) {
			if(spriteName == child.name){
				child.gameObject.SetActive (true);
			} else{
				child.gameObject.SetActive(false);
			}
		}
	}
}



