using UnityEngine;
using System.Collections;

public class MovePlatform : MonoBehaviour
{
//	private float scrollSpeed = 0.01f;
//	private float tileSizeX = 57.6f;
//	
//	private float lerpX;
//	private bool lerping;
//	private int count = 0; 
//	private float endPosition; 
//	private float startPosition;
//	
//	void Start ()
//	{
////		this.startPosition = transform.position.x;
//		this.endPosition = transform.position.x - (15.0f*tileSizeX);
//		lerping = true; 
//	}
//	
//	void Update ()
//	{
//		if (lerping) {
//			Lerp ();
//		}
//	}
//		//float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeX);
//		//float newPosition = Mathf.Lerp(transform.position.x,transform.position.x-tileSizeX,Time.deltaTime * scrollSpeed);
////		Debug.Log ("move platform: " + newPosition);
//		//transform.position = startPosition + Vector3.left * newPosition;
////		Vector3 newPos = new Vector3 (newPosition,transform.position.y,transform.position.z);
////		transform.position = newPos;
//		//Debug.Log (newPosition);
//
//
//	void Lerp(){
////		lerpX = transform.position.x - tileSizeX; 
//
//		float newPosition = Mathf.Lerp(transform.position.x,this.endPosition,Time.deltaTime * scrollSpeed);
//		Vector3 newPos = new Vector3 (newPosition, transform.position.y, transform.position.z);
//		transform.position = newPos;
////		transform.position.Set (newPosition, transform.position.y, transform.position.z);
//
//
//		if (transform.position.x <= this.endPosition + 0.2f) {
//			lerping = false;
//		}
//	}
}