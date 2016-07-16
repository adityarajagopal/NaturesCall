using UnityEngine;
using System.Collections;

public class Rat : Enemy {

	public static Rat instance;

	private bool isJumping;
	private bool isMoving = false;
	private float moveForceX = -10f;
	
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
	void Update () {
	
	}

	void FixedUpdate(){
		move ();
	}

	public void move(){
		if (isMoving) {
			myBody.velocity = new Vector2(moveForceX,myBody.velocity.y);
		}
	}
}
