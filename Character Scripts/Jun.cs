﻿using UnityEngine;
using System.Collections;

public class Jun : Character {

	public static Jun instance;
	public float ladderClimbSpeed;
	public float moveForceX;
	public float jumpForce;
	public float cameraResetSpeed;

	private enum weapon{};
	private bool isMoving = false;
	private bool onSurface = true;
	private bool isSliding = false; 
	private bool climbing = false;
	private bool onLadder = false; 
	private BoxCollider2D target;
	private int level;
	//timer related variables
	private float redZone = -4.9f; 
	private float yellowZone = -1.0f; 
	private float timer = 0.0f;
	private float redTimeout = 7.5f; 
	private float yellowTimeout = 5f; 
	private Vector3 endPos; 
	private bool movingCamera; 


	void Awake(){
		if (instance == null) {
			instance = this;
		}

		//Initialise player components
		myBody = this.GetComponent<Rigidbody2D> (); 
		anim = this.GetComponent<Animator> ();
		boxCollider = this.GetComponent<BoxCollider2D> ();

		boxCollider.sharedMaterial.friction = 10f; 
		isMoving = true;
		this.target = null; 
		this.movingCamera = false; 
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (this.climbing) {
			climbLadder(); 
		}
	}

	void Update	() {
		this.correctPosition (); 
		if (this.movingCamera) {
			this.moveCamera(); 
		}
	}

	public void jump(){
		if (onSurface) {
			if(isSliding){
				this.endSlide(); 
			}
//			myBody.velocity = new Vector2 (myBody.velocity.x, jumpForce);
			else{
				myBody.AddForce(new Vector2(0.0f, jumpForce));
			}
		}
		onSurface = false;
	}

	public void attack(){

	}

	public void slide(){
		if (onSurface) {
			isSliding = true; 
			this.anim.SetBool("isSliding",true); 
			boxCollider.offset = new Vector2 (boxCollider.offset.x,(-0.5f*((boxCollider.size.y/2.0f))-0.2f));
			boxCollider.size = new Vector2(boxCollider.size.y, (boxCollider.size.x/2.0f));
//			boxCollider.sharedMaterial.friction = 100.0f;
			myBody.AddForce(new Vector2(-10000f, 0.0f));
		}
	}

	public void endSlide(){
		Debug.Log ("endSlide called"); 
		this.anim.SetBool ("isSliding", false); 
		boxCollider.offset = new Vector2 (0.0f, 0.0f);
		boxCollider.size = new Vector2((boxCollider.size.y*2.0f),boxCollider.size.x);
		myBody.AddForce(new Vector2(10000f, 0.0f));
		isSliding = false; 
	}
	                    
	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == "Platform") {
			Debug.Log("landed on platform"); 
			onSurface = true;	
		}
		if (target.tag == "Ladder") {
			if(onLadder) return; 
			this.onLadder = true; 
			Debug.Log("bitch I'm climbing a ladder");
			this.transform.parent = null; 
			this.myBody.isKinematic = true;
//			if (Input.GetKeyDown("up")) {
			this.climbing=true; 	
//			}
			this.target = (BoxCollider2D) target; 
		}
	}

	void climbLadder(){
//		Debug.Log ("yo details bitch"); 
//		Debug.Log (this.target.size); 
//		Debug.Log (transform.position);  
		this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x,this.target.size.y + 2.55f - this.transform.position.y), this.ladderClimbSpeed*Time.deltaTime);
		Debug.Log (this.transform.position.y); 
		Debug.Log (this.target.size.y); 
		if (this.transform.position.y >= this.target.size.y/2.0f) {
			Debug.Log("I should be fking done"); 
			this.climbing = false; 
			this.transform.parent = Camera.main.transform; 
			this.myBody.isKinematic = false; 
			this.onLadder = false; 
		}
	}

	void correctPosition(){ 
		if (this.transform.localPosition.x < this.yellowZone /*&& this.transform.localPosition.x > this.redZone + 0.2f*/) {
			//yellow zone
			this.timer += Time.deltaTime; 

			if (this.timer >= this.yellowTimeout) {
				Debug.Log ("shoudl exit yellowTimeout"); 
				//do stuff with camera
//				this.endPos = new Vector3 (Camera.main.transform.position.x + this.transform.localPosition.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
//				this.endPos = new Vector3 (GameManager.instance.camera2.transform.position.x + this.transform.localPosition.x, GameManager.instance.camera2.transform.position.y, GameManager.instance.camera2.transform.position.z);
//				this.transform.parent = null; 
				GameManager.instance.camera2.enabled = true; 
//				Camera.main.enabled = false; 
				GameManager.instance.camera2.GetComponent<MovePlatform>().lerping = false; 
//				MovePlatform.instance.lerping = false; 
				this.movingCamera = true; 
			} 
		} /*else if (this.transform.localPosition.x > this.redZone - 0.2f && this.transform.localPosition.x < this.redZone + 0.2f) {
			//boundary between zones
			this.timer = 0; 
		} else if (this.transform.localPosition.x < this.redZone - 0.2f) { 
			//red zones
			this.timer += Time.deltaTime; 

			if (this.timer >= this.redTimeout) {
				Debug.Log("thank fuck I'm out");
				//do stuff with camera
				Debug.Log(Camera.main.transform.position); 
				this.endPos = new Vector3(Camera.main.transform.position.x - (-2.45f - this.transform.localPosition.x), Camera.main.transform.position.y, Camera.main.transform.position.z); 
				Debug.Log(this.endPos); 
				this.transform.parent = null; 
				MovePlatform.instance.lerping = false; 
				this.movingCamera = true; 
			}
		}*/
	}

	void moveCamera(){
		Debug.Log ("move camera"); 
		Debug.Log (GameManager.instance.camera2.transform.position); 
		Debug.Log (this.transform.position); 
//		float x = Mathf.Lerp(Camera.main.transform.position.x,this.endPos.x,this.cameraResetSpeed*Time.deltaTime); 
//		float x = Mathf.Lerp(GameManager.instance.camera2.transform.position.x,this.endPos.x,this.cameraResetSpeed*Time.deltaTime);
		float x = Mathf.Lerp(GameManager.instance.camera2.transform.position.x,this.transform.position.x,this.cameraResetSpeed*Time.deltaTime); 
//		Camera.main.transform.position = new Vector3 (x, Camera.main.transform.position.y, Camera.main.transform.position.z); 
		GameManager.instance.camera2.transform.position = new Vector3 (x, GameManager.instance.camera2.transform.position.y, GameManager.instance.camera2.transform.position.z); 
		

//		if (Camera.main.transform.position.x <= this.endPos.x + 0.2f) {
//			Debug.Log("here"); 
//			this.transform.parent = Camera.main.transform; 
//			MovePlatform.instance.lerping = true; 
//			this.timer = 0.0f; 	
//		}
		if (GameManager.instance.camera2.transform.position.x <= this.transform.position.x + 0.2f) {
			Debug.Log("here"); 
			this.transform.parent = GameManager.instance.camera2.transform; 
			GameManager.instance.camera2.GetComponent<MovePlatform>().lerping = true;  
			this.timer = 0.0f;
			this.movingCamera = false; 

			Camera.main.GetComponent<MovePlatform>().lerping = false; 
			Camera.main.transform.position = GameManager.instance.camera2.transform.position; 
			this.transform.parent = Camera.main.transform; 
			Camera.main.GetComponent<MovePlatform>().lerping = true; 
			GameManager.instance.camera2.enabled = false; 
			Camera.main.enabled = true; 
		}
	}
	
}
