using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSegment : MonoBehaviour
{
    public GameObject segmentPrefab;
    public GameObject jointPrefab;
    public GameObject segmentEnd;

    public float xmax;
    public float ymax;
    public float zmax;
    
    private void Start() {

        for (int i = 0; i < 1; i++)
        {
            GameObject t = Instantiate(segmentPrefab, new Vector3(Random.Range(0, xmax), Random.Range(0, ymax), Random.Range(0, zmax)), new Quaternion(0, 0, 0, 0));
            Rigidbody rb = t.GetComponent<Rigidbody>();
            HingeJoint j = rb.GetComponent<HingeJoint>();

            j.useMotor = true;
        }

    }
    
}
