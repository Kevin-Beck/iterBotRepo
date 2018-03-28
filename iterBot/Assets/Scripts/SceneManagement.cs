using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
    public void SwitchToRunScene() {
        SceneManager.LoadScene(1);
    }
    public void SwitchToBasicScene() {
        SceneManager.LoadScene(2);
    }
}
