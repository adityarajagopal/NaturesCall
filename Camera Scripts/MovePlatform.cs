using UnityEngine;
using System.Collections;

public class MovePlatform : MonoBehaviour
{
	private float scrollSpeed = 5.0f;
	private float tileSizeX = 57.6f;
	
	private float lerpX;
	private bool lerping;
	private int count = 0; 
	private Vector2 endPosition; 
	private Vector2 newPosition;
	private Vector2 offset;

	void Awake ()
	{
//		this.startPosition = transform.position.x;
		offset = new Vector2 (-15*tileSizeX, 0);
		endPosition = new Vector2(transform.position.x + offset.x,transform.position.y + offset.y);
//		this.endPosition = transform.position.x - (tileSizeX);
		lerping = true; 
	}
	
	void Update ()
	{
		if (lerping) {
			Lerp ();
		} 
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
		transform.position = Vector2.MoveTowards( transform.position, endPosition, scrollSpeed*Time.deltaTime);
		
//		float newPosition = Mathf.Lerp(transform.position.x,this.endPosition,Time.deltaTime * scrollSpeed);
		//newPosition = new Vector2 (newPosition.x, transform.position.y);
		//transform.position = newPosition;


		if (transform.position.x <= endPosition.x + 0.2f) {
			lerping = false;
		}
	}
}