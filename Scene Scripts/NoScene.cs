using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;

public class NoScene : MonoBehaviour {
	public void LoadGameOver() {
		SceneManager.LoadScene ("GameOver"); 
	}
}
