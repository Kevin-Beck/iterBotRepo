using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMenu : MonoBehaviour {
    void Awake() {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start() {
        Debug.Log("Hey");
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey("escape"))
            Application.Quit();   
    }
}
