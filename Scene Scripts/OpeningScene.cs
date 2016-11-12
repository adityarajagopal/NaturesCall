using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using System.Collections;

public class OpeningScene : MonoBehaviour {
	
	public void LoadSewer(){
		SceneManager.LoadScene ("Sewer");  
//		Application.LoadLevel ("Sewer"); 
	}
}
