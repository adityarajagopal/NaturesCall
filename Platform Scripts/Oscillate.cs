using UnityEngine;
using System.Collections;

public class Oscillate : MonoBehaviour {

	public bool movingHorizontally = false;
	public float speed = 2.5f;
	private int direction; 
	private bool moving;

	public float tentacleSpeed; 

	// Use this for initialization
	void Awake () {
		this.direction = 1; 
		this.moving = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.moving) {
			this.move (); 
		}
	}

	public void move(){
		if (!movingHorizontally) {
			if (this.direction == 1) {
//				Debug.Log ("moving up");
				this.transform.position = Vector3.MoveTowards (this.transform.position, this.transform.position + Vector3.up, this.speed * Time.deltaTime); 
			} else if (this.direction == -1) {
//				Debug.Log ("moving down"); 
				this.transform.position = Vector3.MoveTowards (this.transform.position, this.transform.position + Vector3.down, this.speed * Time.deltaTime); 
			}
		} else {
			if (this.direction == 1) {
//				Debug.Log ("moving up");
				this.transform.position = Vector3.MoveTowards (this.transform.position, this.transform.position + Vector3.right, this.speed * Time.deltaTime); 
			} else if (this.direction == -1) {
//				Debug.Log ("moving down"); 
				this.transform.position = Vector3.MoveTowards (this.transform.position, this.transform.position + Vector3.left, this.speed * Time.deltaTime); 
			}
		}
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.tag == "ChangeDirection") {
//			Debug.Log ("hit tentacle collider"); 
			this.direction = this.direction * -1;
		}
	}
}
