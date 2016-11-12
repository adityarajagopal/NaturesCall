using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
	public float scrollSpeed = 5.0f;
	private float tileSizeX = 57.6f;
	
	private float lerpX;
	private int count = 0; 
	private Vector2 endPosition; 
	private Vector2 newPosition;
	private Vector2 offset;
	private GameObject player; 
	private GameObject parentCamera; 
	private GameObject upDetector; 
	private GameObject downDetector; 

	public bool lerpUp; 
	public bool lerpDown; 
	public bool lerping;
	public static MoveCamera instance; 

	void Awake ()
	{
		if (instance == null) {
			instance = this; 
		}
		offset = new Vector2 (15*tileSizeX, 0);
		endPosition = new Vector3(this.transform.position.x + offset.x,this.transform.position.y + offset.y,this.transform.position.z);
		lerping = true; 
		this.lerpUp = false; 
		this.player = GameObject.FindGameObjectWithTag ("Jun"); 
		this.upDetector = GameObject.FindGameObjectWithTag ("UpDetector"); 
		this.downDetector = GameObject.FindGameObjectWithTag ("DownDetector"); 
	}

	void Update ()
	{
//		this.checkPlayerPosition (); 
		if (lerping) {
			Lerp ();
		} 
//		if (lerpUp) {
//			LerpUp (); 
//		}
//		if (lerpDown) {
//			LerpDown() ;
//		}
	}

	void Lerp(){
		this.transform.position = Vector3.MoveTowards(this.transform.position, this.endPosition, scrollSpeed*Time.deltaTime);
	}

	void LerpUp(){ 
		this.player.transform.parent = null; 
		float y = Mathf.Lerp (this.transform.position.y, this.player.transform.position.y, 10f*scrollSpeed * Time.deltaTime);
		this.transform.position = new Vector3 (this.transform.position.x, y, this.transform.position.z); 

		if (this.transform.position.y >= this.player.transform.position.y - 0.2f) {
			if(!this.player.GetComponent<Jun>().climbing){
				this.player.transform.parent = Camera.main.transform; 
				this.lerpUp = false; 
			}
		}
	}

	void LerpDown(){
		this.player.transform.parent = null; 
		float y = Mathf.Lerp (this.transform.position.y, this.player.transform.position.y, 10f*scrollSpeed * Time.deltaTime);
		this.transform.position = new Vector3 (this.transform.position.x, y, this.transform.position.z); 

		if (this.transform.position.y <= 0.1f) {
			this.player.transform.parent = Camera.main.transform; 
			this.lerpDown = false;
		} else if (this.transform.position.y <= this.player.transform.position.y + 0.2f) {
			this.player.transform.parent = Camera.main.transform; 
			this.lerpDown = false; 
		}
	}	
}