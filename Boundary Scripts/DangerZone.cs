using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;

public class DangerZone : MonoBehaviour {
	public bool activated; 
	// Use this for initialization
	void Start () {
		this.activated = true; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == "Jun") {
			if (this.activated) {
				Debug.Log ("GameOver");
//				UnityEditor.EditorApplication.isPlaying = false; 
				GameObject.FindGameObjectWithTag("Jun").GetComponent<Jun>().checkHighScore(); 
				SceneManager.LoadScene ("Continue"); 
			}
		}
	}
}
