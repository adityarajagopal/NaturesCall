using UnityEngine;
using System.Collections;

public class DangerZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == "Jun") {
			Debug.Log ("GameOver");
			UnityEditor.EditorApplication.isPlaying = false; 
		}
	}
}
