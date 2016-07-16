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
	private GameObject platform,
		tutorialPlatform,
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
		finalPlatform;

	//Background
	[SerializeField]
	private GameObject sewerLayer1,
		sewerLayer2,
		sewerLayer4;

	private int numBlocks = 13;
	private int currentBlock = 0;
	private Block[] blockArr;

	//[SerializeField]
	//private GameObject dangerZone;

	private bool lerpingCamera; // Toggles to true when camera is moved
	private float lerpTime = 1.5f; // Time taken for the lerp sequence
	private float lerpX; // x-position of camera after lerping 

	private float minX = -96.0f, minY = -5.40f, maxX = -96.0f, maxY = 5.40f;

	// Use this for initialization
	void Awake () {

		if (instance == null) {
			instance = this;
		}

		//jun = GameObject.FindGameObjectWithTag ("Jun");
		//rat = GameObject.FindGameObjectWithTag ("Rat");
		//sewerLayer1 = GameObject.FindGameObjectWithTag ("Sewer_Layer_1");
		//sewerLayer2 = GameObject.FindGameObjectWithTag ("Sewer_Layer_2");
		//sewerLayer4 = GameObject.FindGameObjectWithTag ("Sewer_Layer_4");

		Debug.Log ("MaxX: " + maxX + "\nMaxY: " + maxY + "\nMinX: " + minX
		    + "\nMinY: " + minY + "\n");

		blockArr = new Block[numBlocks];

		blockArr [2] = new Block ("B", "B", "std", plat1);
		blockArr [3] = new Block ("B", "U", "std", plat2);
		blockArr [4] = new Block ("M", "M", "std", plat3);
		blockArr [5] = new Block ("M", "M", "std", plat4);
		blockArr [6] = new Block ("M", "M", "std", plat5);
		blockArr [7] = new Block ("T", "D", "std", plat6);
		blockArr [8] = new Block ("T", "M", "std", plat7);
		blockArr [9] = new Block ("T", "T", "std", plat8);
		blockArr [10] = new Block ("T", "T", "std", plat9);
		blockArr [11] = new Block ("T", "T", "std", plat10);

		CalcBlockOrder (blockArr);
		CreateInitialPlatforms (blockArr);
	}

	// Update is called once per frame
	void Update () {
//		if (lerpingCamera) {
//			LerpCamera (); 
//		} 
		
		//TrackPlayer (); 
	}

//	void LerpCamera() {
//		float x = Camera.main.transform.position.x; 
//		
//		x = Mathf.Lerp (x, lerpX, lerpTime * Time.deltaTime);  
//		Camera.main.transform.position = new Vector3 (x, Camera.main.transform.position.y, Camera.main.transform.position.z); 
//		
//		if (Camera.main.transform.position.x >= (lerpX - 0.02f)) {
//			lerpingCamera = false;
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

	void CreateInitialPlatforms(Block[] blockArr){
 
		Vector3 junPos = new Vector3 (-8.0f,0.0f,0.0f);
		Vector3 ratPos = new Vector3 (10.0f,0.0f,0.0f);
		Vector3 platformPos = new Vector3 (0.0f,0.0f,0.0f);
		Vector3 platform2Pos = new Vector3 (57.6f, 0.0f, 0.0f);

		Instantiate (junExternal, junPos, Quaternion.identity);
		Instantiate (ratExternal, ratPos, Quaternion.identity);
		Instantiate (blockArr[currentBlock].platform, platformPos, Quaternion.identity);
		Instantiate (blockArr[currentBlock].platform, platform2Pos, Quaternion.identity);
		Instantiate (sewerLayer1, platformPos, Quaternion.identity);
		Instantiate (sewerLayer1, platform2Pos, Quaternion.identity);
		Instantiate (sewerLayer2, platformPos, Quaternion.identity);
		Instantiate (sewerLayer2, platform2Pos, Quaternion.identity);
		Instantiate (sewerLayer4, platformPos, Quaternion.identity);
		Instantiate (sewerLayer4, platform2Pos, Quaternion.identity);

	}

	void CalcBlockOrder(Block[] blockArr){
		blockArr [0] = new Block ("M", "M", "std", platform);
		blockArr [1] = new Block ("B", "B", "tut", tutorialPlatform);

		//Calculate random block order
		Random rnd = new Random ();

		//number = rnd.next (2, 11);

		blockArr [12] = new Block ("T", "B", "fin", finalPlatform);
	}
}

