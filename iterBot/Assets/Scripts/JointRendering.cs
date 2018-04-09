using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointRendering : MonoBehaviour {
    public GameObject baseObject;
    public GameObject targetObject;

    private Transform baseTransform;
    private Transform targetTransform;

    public float speed = 1.0F;
    private float lengthScale = 1.0f;
    public float distanceBetweenBaseAndTarget;

    void Start () {
        baseTransform = baseObject.GetComponent<Transform>();
        targetTransform = targetObject.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        distanceBetweenBaseAndTarget = Vector3.Magnitude(baseTransform.position - targetTransform.position);
        transform.localScale = new Vector3(0.5f, distanceBetweenBaseAndTarget / 2, 0.5f);

        Vector3 CurPos = transform.position;
        Vector3 WhereToGo = (baseTransform.position + targetTransform.position) / 2;
        transform.position = Vector3.Lerp(CurPos, WhereToGo, speed);



        Vector3 relativePos = targetTransform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;
        transform.Rotate(0, 90f, 90f);
    }
}
