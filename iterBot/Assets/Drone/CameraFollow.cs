using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float distance = 4;
    public float height = 3;
    public GameObject target;
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Transform>().position = new Vector3(target.transform.position.x, target.transform.position.y + height, target.transform.position.z - distance);

	}
}
