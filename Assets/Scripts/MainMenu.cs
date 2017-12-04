using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("_SCENE");
    }
}
