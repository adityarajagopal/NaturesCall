using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour
{
	public float scrollSpeed;
	public float tileSizeX;
	
	private Vector3 startPosition;
	
	void Start ()
	{
		startPosition = transform.position;
	}
	
	void Update ()
	{
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeX);
		//float newPosition = Mathf.Lerp(transform.position.x,startPosition.x-tileSizeX,Time.deltaTime * scrollSpeed);
		transform.position = startPosition + Vector3.left * newPosition;
		//transform.position = new Vector3 (newPosition,transform.position.y,transform.position.z);
	}
}