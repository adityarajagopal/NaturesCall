using UnityEngine;
using System.Collections;

public class RopeCollider : MonoBehaviour {

	private GameObject player; 
	private Rigidbody2D myBody;
	private DistanceJoint2D dj; 
	private bool leftRope; 
	private float totalForceAdded;

	public float swingForce; 
	public bool onRope;
	public bool onWheel; 

	public float maxForce = 120000f; 

	// Use this for initialization
	void Awake () {
		this.player = GameObject.FindGameObjectWithTag ("Jun"); 
		this.myBody = this.player.GetComponent<Rigidbody2D> ();
		this.dj = this.player.GetComponent<DistanceJoint2D> (); 

		this.onRope = false; 
		this.leftRope = false; 
		this.totalForceAdded = 0f; 

		this.onWheel = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (onRope) {
			if (!Input.GetKey (KeyCode.A)) {
				Debug.Log ("still onrope"); 
				this.dj.enabled = false; 
				this.onRope = false;
				this.leftRope = true;
			}
			if (Input.GetKeyDown ("right")) {
				this.totalForceAdded += 20000f; 
				if (this.totalForceAdded <= this.maxForce) {
					this.myBody.AddForce (new Vector2 (20000f, 0f)); 
				}
			}
			if (Input.GetKeyDown ("left")) {
				this.totalForceAdded += 20000f; 
				if (this.totalForceAdded <= this.maxForce) {
					this.myBody.AddForce (new Vector2 (-20000f, 0f)); 
				}
			}
		} 
		else if (this.leftRope)
		{
			this.dj.enabled = false;
			this.player.GetComponent<Jun> ().droppedOffRope ();
			this.leftRope = false;
		}
	}

	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == "Djoint") {
			if (onRope)
				return;
			this.onRope = true; 
			Debug.Log ("tarzan cry"); 
			this.player.transform.parent = null; 
			this.totalForceAdded = 0f; 
	
			Camera.main.GetComponent<MoveCamera> ().scrollSpeed = 0.5f; 
			GameObject.FindGameObjectWithTag ("Camera_2").GetComponent<MoveCamera> ().scrollSpeed = 0.5f; 

			this.dj.enabled = true; 
			this.dj.connectedBody = target.attachedRigidbody;   
			this.dj.anchor = new Vector2 (0f, 0f); 
			this.dj.connectedAnchor = new Vector2 (0f, 0f); 
			this.dj.distance = 1f; 
		} else if (target.tag == "Rope") {
			if (onRope)
				return; 
			Debug.Log ("onRope");
			this.player.transform.parent = null;
		}
	}
}
