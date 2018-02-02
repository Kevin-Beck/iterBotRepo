using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObstacles : MonoBehaviour {
    public int numberOfObstacles;
    public float sizeOfObstacles;
    public GameObject typeOfObject;
    public float planeScaleX, planeScaleZ;
    public float height;

    // Use this for initialization
    void Start () {
        float fieldxmin, fieldxmax, fieldzmin, fieldzmax;
        fieldxmin = planeScaleX * -5.0f;
        fieldxmax = planeScaleX * 5.0f;
        fieldzmin = planeScaleZ * -5.0f;
        fieldzmax = planeScaleZ * 5.0f;
        for (int i = 0; i < numberOfObstacles; i++)
        {
            GameObject t = Instantiate(typeOfObject, new Vector3(Random.Range(fieldxmin, fieldxmax), height, Random.Range(fieldzmin, fieldzmax)), new Quaternion(0, 0, 0, 0));
            t.transform.localScale = new Vector3(sizeOfObstacles, sizeOfObstacles, sizeOfObstacles);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
