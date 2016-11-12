using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;

public class Jun : Character {

	public static Jun instance;
	public float ladderClimbSpeed;
	public float moveForceX;
	public float jumpForce;
	public float cameraResetSpeed;
	public bool climbing = false;

	private enum weapon{};
	private bool isMoving = false;
	private bool onSurface = true;
	private bool isSliding = false; 
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
	private float topOfLadder; 
	private float ladderXPos; 
	public bool canCorrect; 
	private bool canSpeedUp; 

	private bool onWall; 
	private bool leftJumped; 
	private bool rightJumped; 

	private bool onWheel; 
	private Vector2 direction; 
	private float axisWhenLanded; 

	private RopeCollider ropeCollider; 
	private DistanceJoint2D dj; 
	private BoxCollider2D collider; 
	private GameObject mop;
	private float attackTimer; 
	private bool attacking; 

	private bool attackedRat; 
	private bool killedRat; 
	public int score; 

	void Awake(){
		if (instance == null) {
			instance = this;
		}

		//Initialise player components
		myBody = this.GetComponent<Rigidbody2D> (); 
		anim = this.GetComponent<Animator> ();
//		this.collider = this.GetComponent<BoxCollider2D> ();
		this.collider = GameObject.FindGameObjectWithTag("JunCollider").GetComponent<BoxCollider2D>(); 

		isMoving = true;
		this.target = null; 
		this.movingCamera = false; 

		this.onWall = false; 
		this.leftJumped = false; 
		this.rightJumped = false; 

		this.GetComponent<DistanceJoint2D> ().enabled = false; 
		this.ropeCollider = GameObject.FindGameObjectWithTag ("RopeCollider").GetComponent<RopeCollider> (); 
		this.dj = this.GetComponent<DistanceJoint2D> (); 
		this.canCorrect = true; 
		this.canSpeedUp = true; 

		this.mop = GameObject.FindGameObjectWithTag ("MopCollider");
		this.mop.SetActive (false);
		this.attackTimer = 0f; 
		this.attacking = false; 

		this.score = 0; 
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (this.climbing && Input.GetKey("up")) {
			climbLadder(); 
		}
	}

	void Update	() {
		if (this.attacking) {
			this.attackTimer += Time.deltaTime; 
		}

		if (!this.attacking) {Debug.Log (this.score);}

		//Setting Animation State Machine Variables
		if (this.onSurface) {
			this.anim.SetBool ("onPlatform", true);
		} else {
			this.anim.SetBool ("onPlatform", false);
		}
		if (this.myBody.velocity.y < 0) {
			this.anim.SetBool ("negVelocity", true);
		} else {
			this.anim.SetBool ("negVelocity", false);
		}
		if (this.onLadder) {
			this.anim.SetBool ("onLadder", true); 
		} else {
			this.anim.SetBool ("onLadder", false); 
		}
		//

		this.correctPosition (); 
		if (this.movingCamera) {
			this.moveCamera (); 
		}
		if (!this.ropeCollider.onRope && !this.leftJumped && !this.rightJumped) {
			if (Input.GetKey ("left")) {
				this.transform.position = Vector2.MoveTowards (this.transform.position, this.transform.position + Vector3.left, 2.5f * Time.deltaTime); 
			}
			if (Input.GetKey ("right")) {
				if (this.canSpeedUp) {
					Camera.main.GetComponent<MoveCamera> ().scrollSpeed += 0.3f; 
					GameObject.FindGameObjectWithTag ("Camera_2").GetComponent<MoveCamera> ().scrollSpeed += 0.3f; 
				}
			}
			if (Input.GetKeyUp ("right")) {
				Camera.main.GetComponent<MoveCamera> ().scrollSpeed = 3.5f; 
				GameObject.FindGameObjectWithTag ("Camera_2").GetComponent<MoveCamera> ().scrollSpeed = 3.5f; 
			}

			if (this.onWheel) {
				if (!Input.GetKey (KeyCode.A)) {
					this.dj.enabled = false; 
					this.transform.parent = Camera.main.transform; 
//				Camera.main.GetComponent<MoveCamera> ().scrollSpeed = 3.5f; 
//				GameObject.FindGameObjectWithTag ("Camera_2").GetComponent<MoveCamera> ().scrollSpeed = 3.5f; 
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			this.attackTimer = 0f; 
			this.attacking = true; 
			this.mop.SetActive (true); 
			this.anim.SetBool("sPressed", true); 
		}
		if (Input.GetKey (KeyCode.S)) {
			if (this.attackTimer >= 1.0f) {
				this.mop.SetActive (false); 
				this.attacking = false; 
				this.anim.SetBool("sPressed", false); 
			}
		}
		if (Input.GetKeyUp (KeyCode.S)) {
			this.mop.SetActive (false); 
			this.attacking = false;
			this.anim.SetBool("sPressed", false); 
		}
	}

	public void jump(){
		if (Input.GetKey (KeyCode.A))
			return;

		if (this.onWheel) {
//			this.dj.enabled = false;
			this.transform.parent = Camera.main.transform; 
//			Camera.main.GetComponent<MoveCamera> ().scrollSpeed = 3.5f; 
//			GameObject.FindGameObjectWithTag ("Camera_2").GetComponent<MoveCamera> ().scrollSpeed = 3.5f; 
		}
		if (onSurface || this.onWheel) {
			if (isSliding) {
				this.endSlide (); 
			} else {
				myBody.AddForce (new Vector2 (0.0f, jumpForce));
			}
		} 
		onSurface = false;
		this.onWheel = false; 
	}

	public void attack(){

	}

	public void slide(){
		if (onSurface) {
			isSliding = true; 
			this.anim.SetBool("isSliding",true); 
			this.collider.offset = new Vector2 (this.collider.offset.x,(-0.5f*((this.collider.size.y/2.0f))-0.2f));
			this.collider.size = new Vector2(this.collider.size.y, (this.collider.size.x/2.0f));

			this.GetComponent<BoxCollider2D> ().enabled = false; 
			GameObject.FindGameObjectWithTag ("RopeCollider").GetComponent<EdgeCollider2D> ().enabled = false; 

			myBody.AddForce(new Vector2(-10000f, 0.0f));
		}
	}

	public void endSlide(){
		Debug.Log ("endSlide called"); 
		this.anim.SetBool ("isSliding", false); 
		this.collider.offset = new Vector2 (0.0f, 0.0f);
		this.collider.size = new Vector2((this.collider.size.y*2.0f),this.collider.size.x);

		this.GetComponent<BoxCollider2D> ().enabled = true;
		GameObject.FindGameObjectWithTag ("RopeCollider").GetComponent<EdgeCollider2D> ().enabled = true; 

		myBody.AddForce(new Vector2(10000f, 0.0f));
		isSliding = false; 
	}

	public void droppedOffRope(){
		Debug.Log ("called"); 
		this.myBody.velocity = new Vector2 (0f, 0f); 
		this.transform.parent = Camera.main.transform; 
		Camera.main.GetComponent<MoveCamera> ().scrollSpeed = 3.5f; 
		GameObject.FindGameObjectWithTag ("Camera_2").GetComponent<MoveCamera> ().scrollSpeed = 3.5f; 
		Camera.main.GetComponent<MoveCamera> ().lerping = true; 
		GameObject.FindGameObjectWithTag ("Camera_2").GetComponent<MoveCamera> ().lerping = true; 
	}

	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == "Platform" && !(target.tag == "Wall")) {
			Debug.Log ("landed on platform");  
			this.transform.parent = Camera.main.transform; 
			Camera.main.GetComponent<MoveCamera> ().scrollSpeed = 3.5f; 
			GameObject.FindGameObjectWithTag ("Camera_2").GetComponent<MoveCamera> ().scrollSpeed = 3.5f; 
			this.transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f)); 
			this.rightJumped = false; 
			this.leftJumped = false;
			onSurface = true;
			this.canCorrect = true; 
		} 
		else if (target.tag == "Wall") {
			Debug.Log ("Wall"); 
			Camera.main.GetComponent<MoveCamera>().scrollSpeed = 1.0f; 
			GameObject.FindGameObjectWithTag("Camera_2").GetComponent<MoveCamera>().scrollSpeed = 1.0f; 
			onSurface = false; 
			this.rightJumped = false; 
			this.leftJumped = false;
		}
		if (target.tag == "Ladder") {
			if(onLadder) return; 
			this.onLadder = true; 
			Debug.Log("bitch I'm climbing a ladder");
			this.transform.parent = null; 
			this.myBody.isKinematic = true;
			this.climbing=true; 	
			this.target = (BoxCollider2D) target; 
			this.topOfLadder = (this.target.size.y/2.0f) + this.target.transform.position.y + 2.3f; 
			this.ladderXPos = this.transform.position.x; 
		}
		if (target.tag == "UpDetector") {
			Camera.main.GetComponent<MoveCamera>().lerpUp = true; 
			GameObject.FindGameObjectWithTag("Camera_2").GetComponent<MoveCamera>().lerpUp = true; 
		}
		if (target.tag == "DownDetector") {
			Camera.main.GetComponent<MoveCamera>().lerpDown = true; 
			GameObject.FindGameObjectWithTag("Camera_2").GetComponent<MoveCamera>().lerpDown = true; 
		}
		if (target.tag == "Wheel") {
			if (onWheel)
				return;
			Debug.Log ("landed on wheel"); 
			this.onWheel = true; 
//			this.dj.connectedBody = target.attachedRigidbody;   
//			this.dj.anchor = new Vector2 (0f, 0f);
//			this.dj.distance = 0.005f; 
		}
		if (target.tag == "Death") {
			this.transform.parent = null; 
			Debug.Log ("nigga got got"); 
			this.checkHighScore (); 
			SceneManager.LoadScene ("Continue"); 

		}
		if (target.tag == "Rat") {
			if (this.attacking) {
				Debug.Log ("hit rat");
				this.score += 10; 
				Collider2D[] ratColliders = target.GetComponents<Collider2D> (); 
				foreach (Collider2D element in ratColliders) {
					element.enabled = false; 
				}
			} else {
				this.transform.parent = null; 
				this.checkHighScore (); 
				SceneManager.LoadScene ("Continue"); 
				Debug.Log ("nigga got got by rat"); 
			}
		}
	}

	void OnTriggerStay2D(Collider2D target){
		if (target.tag == "Platform") {
			this.canSpeedUp = true; 
		}

		if (target.tag == "Wall"){
			this.transform.parent = null; 
			float xForce = -500f;
			float yForce = 900f; 
			if (Input.GetKey ("up")) {
				if (Input.GetKey ("left")) {
					if (!this.leftJumped) {
						Debug.Log ("left wall jump");
						this.myBody.AddForce (new Vector2 (xForce, yForce), ForceMode2D.Impulse); 
						this.leftJumped = true; 
					}
				} 
				if (Input.GetKey ("right")){
					if (!this.rightJumped) {
						Debug.Log ("right wall jump");
						this.myBody.AddForce (new Vector2 (-xForce, yForce), ForceMode2D.Impulse);
						this.rightJumped = true; 
					}
				}
			}
		}
		if (target.tag == "Ramp") {
			GameObject.FindGameObjectWithTag ("JunCollider").transform.rotation = Quaternion.Euler(new Vector3 (0f, 0f, target.transform.rotation.eulerAngles.z)); 
		}
		if (target.tag == "Wheel") {
			Debug.Log ("on wheel"); 
			this.canCorrect = false; 
			if (!Input.GetKey (KeyCode.A)) {
				this.direction = new Vector2 (this.transform.position.x - target.transform.position.x, this.transform.position.y - target.transform.position.y);
				Debug.Log ((target.GetComponent<CircleCollider2D> ().radius / this.direction.magnitude) * this.direction); 
				Debug.Log (this.axisWhenLanded); 
				this.dj.connectedBody = target.attachedRigidbody;   
				this.dj.anchor = new Vector2 (0f, 0f);
				this.dj.distance = 0.005f;
				Debug.Log (-target.transform.rotation.eulerAngles.z);

				float rotateBy = -target.transform.rotation.eulerAngles.z; 
				Debug.Log (rotateBy); 
				this.direction.x = this.direction.x * Mathf.Cos (Mathf.Deg2Rad * rotateBy) - this.direction.y * Mathf.Sin (Mathf.Deg2Rad * rotateBy); 
				this.direction.y = this.direction.x * Mathf.Sin (Mathf.Deg2Rad * rotateBy) + this.direction.y * Mathf.Cos (Mathf.Deg2Rad * rotateBy); 

				Debug.Log ("transformed vector"); 
				this.direction = (target.GetComponent<CircleCollider2D> ().radius / this.direction.magnitude) * this.direction; 
				Debug.Log (this.direction); 
				this.dj.connectedAnchor = (Vector2) this.direction; 
			}

			if (Input.GetKey (KeyCode.A)) {
				Debug.Log ("I'm grabbing on");
//				this.dj.connectedBody = target.attachedRigidbody;   
//				this.dj.anchor = new Vector2 (0f, 0f);
//				this.dj.distance = 0.005f;
				this.dj.enabled = true; 
//				this.onWheel = true; 
				this.transform.parent = null; 

//				Debug.Log (-target.transform.rotation.eulerAngles.z);
//
//				float rotateBy = -target.transform.rotation.eulerAngles.z; 
//				Debug.Log (rotateBy); 
//				this.direction.x = this.direction.x * Mathf.Cos (Mathf.Deg2Rad * rotateBy) - this.direction.y * Mathf.Sin (Mathf.Deg2Rad * rotateBy); 
//				this.direction.y = this.direction.x * Mathf.Sin (Mathf.Deg2Rad * rotateBy) + this.direction.y * Mathf.Cos (Mathf.Deg2Rad * rotateBy); 
//
//				Debug.Log ("transformed vector"); 
//				this.direction = (target.GetComponent<CircleCollider2D> ().radius / this.direction.magnitude) * this.direction; 
//				Debug.Log (this.direction); 
//				this.dj.connectedAnchor = (Vector2) this.direction; 
			}
		}
	}

	void OnTriggerExit2D (Collider2D target){
		if (target.tag == "Platform"){
			this.canSpeedUp = false; 
			Camera.main.GetComponent<MoveCamera> ().scrollSpeed = 3.5f; 
			GameObject.FindGameObjectWithTag ("Camera_2").GetComponent<MoveCamera> ().scrollSpeed = 3.5f; 
		}
	}

	void climbLadder(){ 
		this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x,this.topOfLadder), this.ladderClimbSpeed*Time.deltaTime);

		if (this.transform.position.y >= this.topOfLadder - 1.8f) {
			Debug.Log("I should be fking done"); 
			this.climbing = false; 
			this.transform.parent = Camera.main.transform; 
			this.myBody.isKinematic = false; 
			this.onLadder = false; 
		}
	}

	void correctPosition(){ 
		if (this.onWheel) {
			if (this.transform.localPosition.x > 5.0f) {
				GameManager.instance.camera2.enabled = true; 
				GameManager.instance.camera2.GetComponent<MoveCamera> ().lerping = false; 
				this.movingCamera = true; 
			}
		}

		if (this.canCorrect) {
			if (this.inYellowZone ()) {
				//yellow zone
				this.timer += Time.deltaTime; 
				if (this.timer >= this.yellowTimeout) {
					//				Debug.Log ("shoudl exit yellowTimeout"); 
					//do stuff with camera
					GameManager.instance.camera2.enabled = true; 
					GameManager.instance.camera2.GetComponent<MoveCamera> ().lerping = false;  
					this.movingCamera = true; 
				} 
			} else if (this.inBoundary ()) {
				//boundary between zones
				this.timer = 0; 
			} else if (this.inRedZone ()) { 
				//red zones
				this.timer += Time.deltaTime; 

				if (this.timer >= this.redTimeout) {
					//				Debug.Log("thank fuck I'm out");
					//do stuff with camera
					GameManager.instance.camera2.enabled = true; 
					GameManager.instance.camera2.GetComponent<MoveCamera> ().lerping = false;  
					this.movingCamera = true; 
				}
			}
		}
	}

	void moveCamera(){
//		Debug.Log ("move camera"); 
		float endPos = 0; 

		if (this.inYellowZone() || this.onWheel) {
			endPos = this.transform.position.x; 
			float x = Mathf.Lerp (GameManager.instance.camera2.transform.position.x, endPos, this.cameraResetSpeed * Time.deltaTime); 
			GameManager.instance.camera2.transform.position = new Vector3 (x, GameManager.instance.camera2.transform.position.y, GameManager.instance.camera2.transform.position.z); 
		} else if (this.inRedZone()) {
			float distanceToMove = -(this.transform.localPosition.x + 2.95f); 
			endPos = this.transform.position.x + distanceToMove;
			float x = Mathf.Lerp (GameManager.instance.camera2.transform.position.x, endPos, this.cameraResetSpeed * Time.deltaTime); 
			GameManager.instance.camera2.transform.position = new Vector3 (x, GameManager.instance.camera2.transform.position.y, GameManager.instance.camera2.transform.position.z);  
		}

		if (GameManager.instance.camera2.transform.position.x <= endPos + 0.2f) {
//			Debug.Log("here"); 
			this.transform.parent = GameManager.instance.camera2.transform; 
			GameManager.instance.camera2.GetComponent<MoveCamera>().lerping = true;  
			this.timer = 0.0f;
			this.movingCamera = false; 

			Camera.main.GetComponent<MoveCamera>().lerping = false; 
			Camera.main.transform.position = GameManager.instance.camera2.transform.position; 
			this.transform.parent = Camera.main.transform; 
			Camera.main.GetComponent<MoveCamera>().lerping = true; 
			GameManager.instance.camera2.enabled = false; 
			Camera.main.enabled = true; 
		}
	}

	bool inYellowZone(){
		if (this.transform.localPosition.x < this.yellowZone && this.transform.localPosition.x > this.redZone + 0.2f) {
			return true; 
		} else {
			return false;
		}
	}

	bool inBoundary(){
		if (this.transform.localPosition.x > this.redZone - 0.2f && this.transform.localPosition.x < this.redZone + 0.2f) {
			return true; 
		} else {
			return false; 
		}
	}

	bool inRedZone(){
		if (this.transform.localPosition.x < this.redZone - 0.2f) {
			return true; 
		} else {
			return false; 
		}
	}

	public void checkHighScore(){
		if (PlayerPrefs.HasKey ("HighScore")) {
			if (PlayerPrefs.GetInt ("HighScore") < this.score) {
				PlayerPrefs.SetInt ("HighScore", this.score); 
				PlayerPrefs.SetInt ("ScoreHasChanged", 1); 
			} else {
				PlayerPrefs.SetInt ("ScoreHasChanged", 0); 
			}
		} else {
			PlayerPrefs.SetInt ("HighScore", this.score); 
			PlayerPrefs.SetInt ("ScoreHasChanged", 1); 
		}
	}
}
