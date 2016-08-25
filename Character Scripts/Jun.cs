using UnityEngine;
using System.Collections;

public class Jun : Character {

	public static Jun instance;
	private int level;
	private enum weapon{};
	private bool isMoving = false;
	public float moveForceX;
	public float jumpForce;
	private bool onSurface = true;
	private bool isSliding = false;
	public float ladderClimbSpeed;
	private bool climbing = false; 
	private BoxCollider2D target; 

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
		this.target = null; 

		this.move ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (this.climbing) {
			climbLadder(); 
		}
//		this.move ();
//		Debug.Log (myBody.velocity);
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
		this.anim.SetBool ("isSliding", false); 
		boxCollider.offset = new Vector2 (0.0f, 0.0f);
		boxCollider.size = new Vector2((boxCollider.size.y*2.0f),boxCollider.size.x);
		myBody.AddForce(new Vector2(10000f, 0.0f));
		isSliding = false; 
	}
	                     

	public void move(){
		if (isMoving) {
//			myBody.velocity = new Vector2(moveForceX,myBody.velocity.y);
			myBody.AddForce(new Vector2(moveForceX,0.0f));
//			myBody.AddForceAtPosition(new Vector2(moveForceX,0.0f), new Vector2(0.0f, -boxCollider.size.y/2.0f));
		}
	}

	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == "Platform") {
			onSurface = true;	
		}
		if (target.tag == "Ladder") {
			Debug.Log("bitch I'm climbing a ladder"); 
			this.climbing=true; 
			Debug.Log (myBody.velocity); 
//			myBody.velocity = new Vector2(0.0f, 0.0f);
//			myBody.isKinematic = true; 
			this.target = (BoxCollider2D) target; 
		}
	}

	void climbLadder(){
//		Debug.Log ("yo details bitch"); 
//		Debug.Log (this.target.size); 
//		Debug.Log (transform.position);  
		transform.position = Vector2.MoveTowards(transform.position, new Vector2(0.0f,this.target.size.y), this.ladderClimbSpeed*Time.deltaTime);
		if (transform.position.y >= this.target.size.y - 0.2f) {
			Debug.Log("I should be fking done"); 
			this.climbing = false; 
			myBody.AddForce (new Vector2(moveForceX, 0.0f));
//			myBody.isKinematic = false; 
			Debug.Log (myBody.velocity); 
		}
	}
}
