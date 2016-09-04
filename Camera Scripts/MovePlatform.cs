using UnityEngine;
using System.Collections;

public class MovePlatform : MonoBehaviour
{
	public float scrollSpeed = 5.0f;
	private float tileSizeX = 57.6f;
	
	private float lerpX;
	private int count = 0; 
	private Vector2 endPosition; 
	private Vector2 newPosition;
	private Vector2 offset;

	public bool lerping;
	public static MovePlatform instance; 

	void Awake ()
	{
		if (instance == null) {
			instance = this; 
		}
//		this.startPosition = transform.position.x;
//		offset = new Vector2 (-15*tileSizeX, 0);
		offset = new Vector2 (15*tileSizeX, 0);
//		endPosition = new Vector2(transform.position.x + offset.x,transform.position.y + offset.y);
		endPosition = new Vector3(this.transform.position.x + offset.x,this.transform.position.y + offset.y,this.transform.position.z);
//		this.endPosition = transform.position.x - (tileSizeX);
		lerping = true; 
	}
	
	void Update ()
	{
		if (lerping) {
			Lerp ();
		} 
//		Lerp ();
	}
		//float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeX);
		//float newPosition = Mathf.Lerp(transform.position.x,transform.position.x-tileSizeX,Time.deltaTime * scrollSpeed);
//		Debug.Log ("move platform: " + newPosition);
		//transform.position = startPosition + Vector3.left * newPosition;
//		Vector3 newPos = new Vector3 (newPosition,transform.position.y,transform.position.z);
//		transform.position = newPos;
		//Debug.Log (newPosition);


	void Lerp(){
//		lerpX = transform.position.x - tileSizeX; 
		this.transform.position = Vector3.MoveTowards(this.transform.position, endPosition, scrollSpeed*Time.deltaTime);
		
//		float newPosition = Mathf.Lerp(transform.position.x,this.endPosition,Time.deltaTime * scrollSpeed);
		//newPosition = new Vector2 (newPosition.x, transform.position.y);
		//transform.position = newPosition;


//		if (this.transform.position.x <= endPosition.x - 0.2f) {
//			lerping = false;
//		}
	}
}