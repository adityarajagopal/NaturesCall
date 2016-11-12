using UnityEngine;
using System.Collections;
using System.Collections.Generic; 


// Data structure representing the different types of platform
public struct Details{
	public string entry; // Height at which the character enters the block
	public string exit;	// Height at which the character leaves the block
	public string platformType; // A platform can be a tutorial, standard or final platform
	public GameObject platform; 

	public Details(string i_entry, string i_exit, string i_platformType, GameObject i_platform){
		entry = i_entry;
		exit = i_exit;
		platformType = i_platformType;
		platform = i_platform;
	}
}

public abstract class Platform : MonoBehaviour {

	protected Details details;  
	protected Dictionary<Enemy,Vector2> enemyPos; 
	protected Dictionary<string,GameObject> triggers; 

	public Details getDetails(){
		return this.details; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
