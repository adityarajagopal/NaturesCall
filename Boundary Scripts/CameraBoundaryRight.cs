using UnityEngine;
using System.Collections;

public class CameraBoundaryRight : MonoBehaviour {

	private bool isLerping = false; 
	private float endPosition; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.isLerping) {
			Lerp(); 
		}
	}

	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == "Jun") {
			this.endPosition = Camera.main.transform.position.x + 57.6f;
			this.isLerping = true; 
		}
	}

	void Lerp(){
		float currentPos = Camera.main.transform.position.x; 
	
//		float newPos = Mathf.Lerp (currentPos, currentPos + 57.6f, 0.2f * Time.deltaTime);
		float newPos = Mathf.Lerp (currentPos, this.endPosition, 0.2f * Time.deltaTime);
		Camera.main.transform.position = new Vector3 (newPos, Camera.main.transform.position.y, Camera.main.transform.position.z);

		if (Camera.main.transform.position.x >= this.endPosition - 25f) {
			Debug.Log("end");
			this.isLerping = false; 
		}
	}
}
