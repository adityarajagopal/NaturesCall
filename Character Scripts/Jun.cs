using UnityEngine;
using System.Collections;

public class Jun : Character {

	public static Jun instance;
	private int level;
	private enum weapon{};
	private bool isMoving = false;
	private float moveForceX = 5f;
	public float jumpForce;
	private bool onSurface = true;

	void Awake(){
		if (instance == null) {
			instance = this;
		}

		//Initialise player components
		myBody = this.GetComponent<Rigidbody2D> (); 
		anim = this.GetComponent<Animator> ();
		boxCollider = this.GetComponent<BoxCollider2D> ();

		boxCollider.sharedMaterial.friction = 0f; 
		isMoving = true;

		this.move ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
//		move ();
	}

	public void jump(){
		if (onSurface) {
			myBody.velocity = new Vector2 (myBody.velocity.x, jumpForce);
		}
		onSurface = false;
	}

	public void attack(){

	}

	public void slide(){
		if (onSurface) {
			boxCollider.offset = new Vector2 (boxCollider.offset.x,(-0.5f*((boxCollider.size.y/2.0f))-0.2f));
			boxCollider.size = new Vector2(boxCollider.size.y, (boxCollider.size.x/2.0f));
//			boxCollider.sharedMaterial.friction = 100.0f;
			myBody.AddForce(new Vector2(-10000f, 0.0f));
		}
	}

	public void move(){
		if (isMoving) {
			myBody.velocity = new Vector2(moveForceX,myBody.velocity.y);
//			myBody.AddForceAtPosition(new Vector2(moveForceX,0.0f), new Vector2(0.0f, -boxCollider.size.y/2.0f));
		}
	}

	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == "Platform") {
			onSurface = true;	
		}
	}

	// public void moveLeft(){}
	// public void moveRight(){}
	// Cannot read other behaviour from the UML diagram

}
