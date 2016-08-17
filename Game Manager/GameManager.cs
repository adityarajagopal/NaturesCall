using UnityEngine;
using System.Collections;

// Data structure representing the different types of platform
public class Block{
	public string entry; // Height at which the character enters the block
	public string exit;	// Height at which the character leaves the block
	public string platformType; // A platform can be a tutorial, standard or final platform
	public GameObject platform; 
	
	public Block(string i_entry, string i_exit, string i_platformType, GameObject i_platform){
		entry = i_entry;
		exit = i_exit;
		platformType = i_platformType;
		platform = i_platform;
	}
}

public class GameManager : MonoBehaviour {
	
	public static GameManager instance;

	[SerializeField]
	private GameObject junExternal;

	[SerializeField]
	private GameObject ratExternal;

	[SerializeField]
	private GameObject tutorialPlatform,
		plat1,
		plat2,
		plat3,
		plat4,
		plat5,
		plat6,
		plat7,
		plat8,
		plat9,
		plat10,
		plat11,
		plat12,
		plat13,
		plat14,
		plat15,
		plat16,
		plat17,
		plat18,
		plat19,
		finalPlatform,
		upLadderM,
		upLadderT,
		upLadderB,
		downSlideM,
		downSlideT,
		downSlideB,
		ladder,
		slide;

	//Background
	[SerializeField]
	private GameObject sewerLayer1,
		sewerLayer2,
		sewerLayer4;

	[SerializeField]
	private Vector3 ladderWidth = new Vector3 (3.0f, 0, 0);

	[SerializeField]
	private Vector3 slideWidth = new Vector3 (3.0f, 0, 0);

	private int numPlatforms = 21;
	private int levelSize = 15;
	private Block[] blockArr;

	private Block[] platforms;
	//[SerializeField]
	//private GameObject dangerZone;

	private bool lerpingCamera; // Toggles to true when camera is moved
	private float lerpTime = 1.5f; // Time taken for the lerp sequence
	private float lerpY = 0.0f; // x-position of camera after lerping 
	private float[] lerpY_list;  
	private float count = 0.0f; 

	private float minX = -96.0f, minY = -5.40f, maxX = -96.0f, maxY = 5.40f;

	// Use this for initialization
	void Awake () {

		if (instance == null) {
			instance = this;
		}

//		Debug.Log ("MaxX: " + maxX + "\nMaxY: " + maxY + "\nMinX: " + minX
//		    + "\nMinY: " + minY + "\n");

		blockArr = new Block[levelSize];
		platforms = new Block[numPlatforms];
		lerpY_list = new float[levelSize];

		platforms [0] = new Block ("B", "B", "std", plat1);
		platforms [1] = new Block ("B", "B", "tut", tutorialPlatform);
		platforms [2] = new Block ("B", "M", "std", plat2);
		platforms [3] = new Block ("B", "M", "std", plat3);
		platforms [4] = new Block ("B", "T", "std", plat4);
		platforms [5] = new Block ("B", "U", "std", plat5);
		platforms [6] = new Block ("M", "B", "std", plat6);
		platforms [7] = new Block ("M", "B", "std", plat7);
		platforms [8] = new Block ("M", "D", "std", plat8);
		platforms [9] = new Block ("M", "M", "std", plat9);
		platforms [10] = new Block ("M", "T", "std", plat10);
		platforms [11] = new Block ("M", "T", "std", plat11);
		platforms [12] = new Block ("M", "U", "std", plat12);
		platforms [13] = new Block ("T", "B", "std", plat13);
		platforms [14] = new Block ("T", "D", "std", plat14);
		platforms [15] = new Block ("T", "D", "fin", finalPlatform);
		platforms [16] = new Block ("T", "M", "std", plat15);
		platforms [17] = new Block ("T", "M", "std", plat16);
		platforms [18] = new Block ("T", "T", "std", plat17);
		platforms [19] = new Block ("T", "U", "std", plat18);
		platforms [20] = new Block ("T", "U", "std", plat19);

		CalcBlockOrder (blockArr);
		LoadLevel (blockArr);
	}

	// Update is called once per frame
	void Update () {
		Jun.instance.move ();

		if (Input.GetKeyDown("up")) {
			Jun.instance.jump();
		}

//		if (lerpingCamera) {
//			LerpCamera (); 
//		} 
		//TrackPlayer (); 
//		if (Input.GetKeyDown ("up")) {
//			lerpingCamera = true;
//			Debug.Log("up pressed: " + Camera.main.transform.position.y);
//			if(Camera.main.transform.position.y < -0.02){
//				this.lerpY = 0.0f;
//			}
//			else {
//				Debug.Log("entered");
//				this.lerpY = 10.80f;
//			}
//			//to_delete = GameObject.FindGameObjectWithTag("Current");
//		} else if (Input.GetKeyDown ("down")) {
//			lerpingCamera = true;
//			Debug.Log("down pressed: " + Camera.main.transform.position.y);
//			if(Camera.main.transform.position.y >= 10.75f)
//				this.lerpY = 0.0f;
//			else 
//				this.lerpY = -10.8f;
//		}
	}

//	void LerpCamera() {
//		float y = Camera.main.transform.position.y; 
//		
//		y = Mathf.Lerp (y,this.lerpY,lerpTime * Time.deltaTime);  
//		Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x,y,Camera.main.transform.position.z); 
//		 
//		if (this.lerpY >= 0) {
//			if (Camera.main.transform.position.y >= (this.lerpY - 0.02f)) {
//				lerpingCamera = false;
//			}
//		} else {
//			if (Camera.main.transform.position.y <= (this.lerpY + 0.02f)) {
//				lerpingCamera = false;
//			}
//		}
//	}
//	void TrackPlayer() {
//		float currentY = Camera.main.transform.position.y;
//		
//		if (jun != null) {
//			currentY = Mathf.Lerp (currentY, jun.transform.position.y, 8f * Time.deltaTime);
//		}
//		
//		Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, currentY, Camera.main.transform.position.z);
//	}

	void LoadLevel(Block[] blockArr){
 
		Vector3 junPos = new Vector3 (-8.0f,0.0f,0.0f);
		Vector3 ratPos = new Vector3 (10.0f,0.0f,0.0f);
		Vector3 platformPos = new Vector3 (0.0f,0.0f,0.0f);
		Vector3 platform2Pos = new Vector3 (57.6f, 0.0f, 0.0f);
		Vector3 increment = new Vector3(0,0,0);

		Instantiate (junExternal, junPos, Quaternion.identity);
//		Instantiate (ratExternal, ratPos, Quaternion.identity);
		Instantiate (sewerLayer1, platformPos, Quaternion.identity);
		Instantiate (sewerLayer1, platform2Pos, Quaternion.identity);
		Instantiate (sewerLayer2, platformPos, Quaternion.identity);
		Instantiate (sewerLayer2, platform2Pos, Quaternion.identity);
		Instantiate (sewerLayer4, platformPos, Quaternion.identity);
		Instantiate (sewerLayer4, platform2Pos, Quaternion.identity);

		Instantiate (blockArr[0].platform, platformPos, Quaternion.identity);
//		lerpY_list [0] = 0.0f; 

		for (int i=1; i<levelSize; i++) {
			Vector3 temp = new Vector3(0,5.4f,0);
			if(blockArr[i-1].exit == "U"){
				increment = new Vector3(57.6f,10.8f,0);
				Instantiate (blockArr[i].platform,platformPos+increment,Quaternion.identity);
//				lerpY_list[i] = 10.8f;

				if(blockArr[i].entry == "B"){
					//Instantiate (upLadderB,platformPos+increment-ladderWidth,Quaternion.identity); 
					Instantiate (upLadderB,platformPos+(increment/2.0f)+temp,Quaternion.identity); 
//					Instantiate (upLadderB,platformPos,Quaternion.identity); 
				}
				else if(blockArr[i].entry == "M"){
					//Instantiate(upLadderM,platformPos+increment-ladderWidth,Quaternion.identity);
					Instantiate(upLadderM,platformPos+(increment/2.0f)+temp,Quaternion.identity);
//					Instantiate(upLadderM,platformPos,Quaternion.identity);
				}
				else if(blockArr[i].entry == "T"){
					//Instantiate(upLadderT,platformPos+increment-ladderWidth,Quaternion.identity);
					Instantiate(upLadderT,platformPos+(increment/2.0f)+temp,Quaternion.identity);
//					Instantiate(upLadderT,platformPos,Quaternion.identity);
				}
			}
			else if(blockArr[i-1].exit == "D"){
				increment = new Vector3(57.6f,-10.8f,0);
				Instantiate (blockArr[i].platform,platformPos+increment,Quaternion.identity);
//				lerpY_list[i] = -10.8f;

				if(blockArr[i].entry == "B"){
					//Instantiate (downSlideB,platformPos+increment-slideWidth,Quaternion.identity); 
					Instantiate (downSlideB,platformPos+(increment/2.0f)+temp,Quaternion.identity); 
//					Instantiate (downSlideB,platformPos,Quaternion.identity); 
				}
				else if(blockArr[i].entry == "M"){
					//Instantiate(downSlideM, platformPos+increment-slideWidth, Quaternion.identity);
					Instantiate(downSlideM, platformPos+(increment/2.0f)+temp, Quaternion.identity);
//					Instantiate(downSlideM, platformPos, Quaternion.identity);
				}
				else if(blockArr[i].entry == "T"){
					//Instantiate(downSlideT, platformPos+increment-slideWidth, Quaternion.identity);
					Instantiate(downSlideT, platformPos+(increment/2.0f)+temp, Quaternion.identity);
//					Instantiate(downSlideT, platformPos, Quaternion.identity);
				}
			}
			else{
				increment = new Vector3(57.6f,0,0);
				Instantiate (blockArr[i].platform, platformPos+increment, Quaternion.identity);//platformPos+increment, Quaternion.identity);
//				lerpY_list[i] = 0.0f; 

				if(blockArr[i-1].exit == "T"){
					if(blockArr[i].entry == "B"){
						//Instantiate (slide, platformPos+increment-slideWidth, Quaternion.identity);
						Instantiate (slide, platformPos+(increment/2.0f), Quaternion.identity);
					}
				}
				else if(blockArr[i-1].exit == "B"){
					if(blockArr[i].entry == "T"){
						//Instantiate (ladder, platformPos+increment-ladderWidth, Quaternion.identity);
						Instantiate (ladder, platformPos+(increment/2.0f), Quaternion.identity);
					}
				}
			}
			platformPos = platformPos + increment; 
			Debug.Log (platformPos);
		}
//		platformPos = platformPos + increment; 
//		Debug.Log (platformPos); 

	}

	void CalcBlockOrder(Block[] blockArr){
		blockArr [0] = new Block ("B", "B", "tut", tutorialPlatform);
		//Random rnd = new Random ();
		int index; 
				
		for (int count=1; count<levelSize-1; count++) {
			index = Random.Range (0,platforms.Length-1); 
			blockArr[count] = platforms[index]; 
		}
		
		blockArr [levelSize-1] = new Block ("T", "B", "fin", finalPlatform);
		
	}
}

