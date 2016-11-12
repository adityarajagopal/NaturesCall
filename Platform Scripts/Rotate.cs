using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float speed = 50f; 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(new Vector3(0f,0f,-this.speed*Time.deltaTime)); 
	}
}
