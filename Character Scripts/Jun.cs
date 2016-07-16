using UnityEngine;
using System.Collections;

public class Jun : Character {

	public static Jun instance;
	private int level;
	private enum weapon{};
	private bool isMoving = false;
	private float moveForceX = 5f;

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
	}

	public void jump(){

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

	// public void moveLeft(){}
	// public void moveRight(){}
	// Cannot read other behaviour from the UML diagram

}
