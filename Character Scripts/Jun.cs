using UnityEngine;
using System.Collections;

public class Jun : Character {

	public static Jun instance;
	private int level;
	private enum weapon{};
	private bool isMoving = false;
	private float moveForceX = 1f;
	public float jumpForce;
	private bool canJump = true;
	private bool lerping = false;
	private Vector2 lerpTo; 

	void Awake(){
		if (instance == null) {
			instance = this;
		}

		//Initialise player components
		myBody = GetComponent<Rigidbody2D> (); 
		anim = GetComponent<Animator> ();

		isMoving = true;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		move ();
		if (lerping) {
			Lerp ();
		}
	}

	public void jump(){
			if (canJump) {
				myBody.velocity = new Vector2 (myBody.velocity.x, jumpForce);
			}
			canJump = false;
	}

	public void attack(){

	}

	public void slide(){
	
	}

	public void move(){
		if (isMoving) {
			myBody.velocity = new Vector2(moveForceX,myBody.velocity.y);
		}
	}

	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == "Platform") {
			canJump = true;	
		} else if (target.tag == "Ladder") {
			//change animation
			Debug.Log("ladder"); 
			lerping = true; 
			this.lerpTo = target.offset; 
			Debug.Log(target.offset);
		}
	}

	void Lerp(){
		float lerpY = Mathf.Lerp(transform.position.y, lerpTo.y+0.4f, Time.deltaTime*0.4f); 
//		transform.position.Set (transform.position.x, lerpY, transform.position.z);
		Vector3 newPos = new Vector3 (transform.position.x, lerpY, transform.position.z);
		transform.position = newPos;

		if (transform.position.y >= lerpY - 0.02f) {
			lerping = false;
		}
	}

	// public void moveLeft(){}
	// public void moveRight(){}
	// Cannot read other behaviour from the UML diagram

}
