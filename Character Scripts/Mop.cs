using UnityEngine;
using System.Collections;

public class Mop : MonoBehaviour {

	private bool attackedRat; 
	private BoxCollider2D[] ratColliders;

	void Awake () {
		this.attackedRat = false; 
		this.ratColliders = new BoxCollider2D[2]; 
	}

	// Update is called once per frame
	void Update () {
		Debug.Log ("script being run"); 
	}

	void OnTrigger2DEnter (Collider2D target){
		if (target.tag == "Rat") {
			Debug.Log ("hit rat"); 
			if (this.attackedRat)
				return;
			this.attackedRat = true; 

			this.ratColliders = target.GetComponents<BoxCollider2D> (); 

			foreach (BoxCollider2D element in this.ratColliders) {
				element.enabled = false; 
			}
			this.attackedRat = false; 
		}
	}
}
