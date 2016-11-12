using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

public class LeaderboardManager : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GameObject.FindGameObjectWithTag ("Name").GetComponent<Text> ().text = PlayerPrefs.GetString ("Name"); 
		GameObject.FindGameObjectWithTag ("Score").GetComponent<Text> ().text = PlayerPrefs.GetInt ("HighScore").ToString(); 
	}
}
