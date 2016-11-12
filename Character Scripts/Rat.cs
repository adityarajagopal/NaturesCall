using UnityEngine;
using System.Collections;

public class Rat : Enemy {

	public int direction; 
	private bool moving; 

	public static Rat instance;
	public float ratSpeed; 
		
	void Awake(){
		if (instance == null) {
			instance = this;
		}
		
		//Initialise player components
		myBody = GetComponent<Rigidbody2D> (); 
		anim = GetComponent<Animator> ();

		this.direction = -1;
		this.ratSpeed = 2.5f;
		this.moving = true; 
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		if (this.moving) {
			move ();
		}
	}

	public void move(){
		if (this.direction == 1) {
//			Debug.Log ("moving right");
			this.transform.position = Vector3.MoveTowards (this.transform.position, this.transform.position + Vector3.right, this.ratSpeed * Time.deltaTime); 
		} else if (this.direction == -1) {
//			Debug.Log ("moving left"); 
			this.transform.position = Vector3.MoveTowards (this.transform.position, this.transform.position + Vector3.left, this.ratSpeed * Time.deltaTime); 
		}
//		this.myBody.AddForce(new Vector2(this.direction*this.ratSpeed, 0f)); 
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.tag == "ChangeDirection") {
			Debug.Log ("hit collider"); 
			this.direction = this.direction * -1;
			this.transform.Rotate (new Vector3 (0f, 180f, 0f)); 
		}
	}
}
