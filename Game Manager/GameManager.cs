using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
//		dangerZone,
//		cameraBoundaryRight;

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

		Debug.Log ("MaxX: " + maxX + "\nMaxY: " + maxY + "\nMinX: " + minX
		    + "\nMinY: " + minY + "\n");

		blockArr = new Block[levelSize];
		platforms = new Block[numPlatforms];
		lerpY_list = new float[levelSize];

		platforms [0] = new Block ("B", "B", "tut", tutorialPlatform);
		platforms [1] = new Block ("B", "B", "std", plat1);
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
		platforms [15] = new Block ("T", "M", "std", plat15);
		platforms [16] = new Block ("T", "M", "std", plat16);
		platforms [17] = new Block ("T", "T", "std", plat17);
		platforms [18] = new Block ("T", "U", "std", plat18);
		platforms [19] = new Block ("T", "U", "std", plat19);
		platforms [20] = new Block ("T", "D", "fin", finalPlatform);

//		dangerZone = GameObject.FindGameObjectWithTag ("DangerZone");


		CalcBlockOrder (blockArr);
		LoadLevel (blockArr);


	}

	// Update is called once per frame
	void Update () {
//		Jun.instance.move ();
		if (Input.GetKeyDown ("up")) {
			Jun.instance.jump ();
		} else if (Input.GetKeyDown ("down")) {
			Jun.instance.slide (); 
		} else if (Input.GetKeyUp ("down")) {
			Jun.instance.endSlide(); 
		}
	}
	
	void LoadLevel(Block[] blockArr){
 
		Vector3 junPos = new Vector3 (-8.0f,0.0f,0.0f);
		Vector3 ratPos = new Vector3 (10.0f,0.0f,0.0f);
		Vector3 platformPos = new Vector3 (0.0f, 0.0f, 0.0f); 
		Vector3 platformPos1 = new Vector3 (0.0f,0.0f,-10.0f);
		Vector3 platformPos2 = new Vector3 (0.0f, 0.0f, 0.0f); 
		Vector3 platformPos3 = new Vector3 (0.0f, 0.0f, 10.0f); 
		Vector3 platformWidth = new Vector3 (57.6f, 0.0f, 0.0f);
		Vector3 increment = new Vector3(0,0,0);

//		Instantiate (dangerZone); 
//		Instantiate (cameraBoundaryRight);

//		Instantiate (junExternal, junPos, Quaternion.identity);
//		Debug.Log ("Instantiated Jun");
//		Instantiate (ratExternal, ratPos, Quaternion.identity);
		for (int i=0; i<15; i++) {
			Instantiate (sewerLayer1, platformPos, Quaternion.identity);
			Debug.Log ("Instantiated sewerLayer1");
			Instantiate (sewerLayer1, platformPos, Quaternion.identity);
			Debug.Log ("Instantiated sewerLayer1(2)");
			Instantiate (sewerLayer2, platformPos, Quaternion.identity);
			Debug.Log ("Instantiated sewerLayer2");
			Instantiate (sewerLayer2, platformPos, Quaternion.identity);
			Debug.Log ("Instantiated sewerLayer2(2)");
//			if((i % 4) == 0){
				Instantiate (sewerLayer4, platformPos, Quaternion.identity);
				Debug.Log ("Instantiated sewerLayer4");
				Instantiate (sewerLayer4, platformPos+platformWidth, Quaternion.identity);
				Debug.Log ("Instantiated sewerLayer4(2)");
//			}
			platformPos += platformWidth;
		}

		platformPos = Vector3.zero;
		Instantiate (blockArr[0].platform, platformPos, Quaternion.identity);
		Debug.Log ("Instantiated blockArr[0]");
//		lerpY_list [0] = 0.0f; 

		for (int i=1; i<levelSize; i++) {
			Vector3 temp = new Vector3(0,5.4f,0);
			if(blockArr[i-1].exit == "U"){
				increment = new Vector3(57.6f,10.8f,0);
				Instantiate (blockArr[i].platform,platformPos+increment,Quaternion.identity);
				Debug.Log ("Instantiated blockArr[" + i + "]");

				if(blockArr[i].entry == "B"){
					Instantiate (upLadderB,platformPos+(increment/2.0f)+temp,Quaternion.identity); 
					Debug.Log ("Instantiated upLadderB");
				}
				else if(blockArr[i].entry == "M"){
					Instantiate(upLadderM,platformPos+(increment/2.0f)+temp,Quaternion.identity);
					Debug.Log ("Instantiated upLadderM");
				}
				else if(blockArr[i].entry == "T"){
					Instantiate(upLadderT,platformPos+(increment/2.0f)+temp,Quaternion.identity);
					Debug.Log ("Instantiated upLadderT");
				}
			}
			else if(blockArr[i-1].exit == "D"){
				increment = new Vector3(57.6f,-10.8f,0);
				Instantiate (blockArr[i].platform,platformPos+increment,Quaternion.identity);
				Debug.Log ("Instantiated blockArr[" + i + "]");

				if(blockArr[i].entry == "B"){
//					Instantiate (downSlideB,platformPos+(increment/2.0f)+temp,Quaternion.identity); 
//					Debug.Log ("Instantiated downSlideB");
				}
				else if(blockArr[i].entry == "M"){
					Instantiate(downSlideM, platformPos+(increment/2.0f)+temp, Quaternion.identity); //doesn't crash with this
					Debug.Log ("Instantiated downSlideM");
				}
				else if(blockArr[i].entry == "T"){
//					Instantiate(downSlideT, platformPos+(increment/2.0f)+temp, Quaternion.identity);
//					Debug.Log ("Instantiated downSlideT");
				}
			}
			else{
				increment = new Vector3(57.6f,0,0);
				Instantiate (blockArr[i].platform, platformPos+increment, Quaternion.identity);//platformPos+increment, Quaternion.identity);
//				Debug.Log ("Instantiated blockArr[" + i + "]");

				if(blockArr[i-1].exit == "T"){
					if(blockArr[i].entry == "B"){
//						Instantiate (slide, platformPos+(increment/2.0f), Quaternion.identity);
//						Debug.Log ("Instantiated slide");
					}
				}
				else if(blockArr[i-1].exit == "B"){
					if(blockArr[i].entry == "T"){
						Instantiate (ladder, platformPos+(increment/2.0f), Quaternion.identity);
						Debug.Log ("Instantiated ladder");
					}
				}
			}
			platformPos = platformPos + increment; 
		}
	}

	void CalcBlockOrder(Block[] blockArr){
		List<int> repeatIndices = new List<int> ();
		blockArr [0] = platforms[0];
		repeatIndices.Add (0); 
		blockArr [1] = platforms [13]; 
		repeatIndices.Add (13); 
		int index; 
				
		for (int count=1; count<levelSize-1; count++) {
			do
			{
				index = Random.Range(1,numPlatforms-1); 
			}
			while(repeatIndices.Contains (index));

			repeatIndices.Add (index);
			Debug.Log (count);
			blockArr[count] = platforms[index]; 
		}
//		blockArr [0] = platforms [0];
//		blockArr [1] = platforms [1];
//		blockArr [2] = platforms [2];
//		blockArr [3] = platforms [3];
//		blockArr [4] = platforms [4];
//		blockArr [5] = platforms [5];
//		blockArr [6] = platforms [6];
//		blockArr [7] = platforms [7];
//		blockArr [8] = platforms [8];
//		blockArr [9] = platforms [9];
//		blockArr [10] = platforms [10];
//		blockArr [11] = platforms [11];
//		blockArr [12] = platforms [12];
//		blockArr [13] = platforms [13];
//		blockArr [14] = platforms [14];

		
		blockArr [levelSize - 1] = platforms [numPlatforms-1];
		
	}
}

