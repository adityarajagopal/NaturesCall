using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 
using System.Collections;

public class LeaderBoard : MonoBehaviour {

	private InputField name; 

	void Awake(){
		this.name = GameObject.FindGameObjectWithTag ("Name").GetComponent <InputField>(); 
	}

	public void UpdateName() {
		Debug.Log (this.name.text); 
	}

	public void GoToLeaderboard() {
		if (PlayerPrefs.GetInt ("ScoreHasChanged") == 1) {
			PlayerPrefs.SetString ("Name", this.name.text); 
		} 

		SceneManager.LoadScene ("Leaderboard"); 

	}
}
