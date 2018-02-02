using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppositeUpperLegMechanics : MonoBehaviour {
    bool MovingUp;
    bool MovingForward;
    public float ROM = 1;
    // The front stop is the range at which forward motion is changed to backwards
    // Inside being the closest to the center point, outside being the outter range aka, the max rotation of the leg
    // back stop is where the back motion is turned into 
    private float frontStopInside;
    private float frontStopOutside;
    private float backStopInside;
    private float backStopOutside;
    private float topStopInside;
    private float topStopOutside;
    private float bottomStopInside;
    private float bottomStopOutside;
    public float variance; // variance is used to create the spread 

    //legstates
    // Moving up = 1, then the leg is activing moving up
    // Moving up = 0, then the leg is activing moving down
    // same for the moving forward.
    private Rigidbody rb;
    private ConfigurableJoint j;
    private SoftJointLimit sjl;

    public float strength;
    public float testing;


    void Start() {
        rb = GetComponent<Rigidbody>();
        j = rb.GetComponent<ConfigurableJoint>();
        sjl = new SoftJointLimit();
        sjl.limit = 90 * ROM;
        j.angularYLimit = sjl;
        j.angularZLimit = sjl;
        frontStopOutside = 270 - (90 - ROM * 90);
        frontStopInside = frontStopOutside - variance * ROM;
        backStopOutside =  90 + 90-(ROM * 90);
        backStopInside = backStopOutside + variance * ROM;

        topStopOutside = 90 - (90 - (ROM * 90));
        topStopInside = topStopOutside - variance * ROM;
        bottomStopOutside = 360  - (ROM * 90);
        bottomStopInside = frontStopOutside - variance * ROM;

    }
    // Update is called once per frame
    void FixedUpdate() {

        if (MovingForward)
        {
            rb.AddTorque(strength * (new Vector3(0, 1f, 0)));
        }
        else
        {
            rb.AddTorque(strength * (new Vector3(0, -1f, 0)));
        }

        // Check if we are in the range for the backstop
        if (rb.transform.eulerAngles.y > (backStopOutside) && rb.transform.eulerAngles.y < (backStopInside))
        {
            MovingForward = true;
        }
        else
        if (rb.transform.eulerAngles.y > (frontStopInside) && rb.transform.eulerAngles.y < (frontStopOutside))
        {
            MovingForward = false;
        }
        

        if (MovingUp)
        {
            rb.AddTorque(strength * (new Vector3(0, 0, -1f)));
        }
        else
        {
            rb.AddTorque(strength * (new Vector3(0, 0, 1f)));
        }

        testing = rb.transform.eulerAngles.y;
       
        if (rb.transform.eulerAngles.z > (bottomStopInside) && rb.transform.eulerAngles.z < (bottomStopOutside))
        {
            MovingUp = true;
        }else
        if (rb.transform.eulerAngles.z > (topStopInside) && rb.transform.eulerAngles.z < (topStopOutside))
        {
            MovingUp = false;
        }
        

    }
}
