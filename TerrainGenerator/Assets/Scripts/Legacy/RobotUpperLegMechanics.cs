using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotUpperLegMechanics : MonoBehaviour {
    
    bool MovingUp;
    bool MovingForward;
    public float ROM = 1;
    // The front stop is the range at which forward motion is changed to backwards
    // Inside being the closest to the center point, outside being the outter range aka, the max rotation of the leg
    // back stop is where the back motion is turned into 
    public float frontStopInside;
    public float frontStopOutside;
    public float backStopInside;
    public float backStopOutside;
    public float topStopInside;
    public float topStopOutside;
    public float bottomStopInside;
    public float bottomStopOutside;
    public float variance; // variance is used to create the spread 

    //legstates
    // Moving up = 1, then the leg is activing moving up
    // Moving up = 0, then the leg is activing moving down
    // same for the moving forward.
    private Rigidbody rb;
    private ConfigurableJoint j;
    private SoftJointLimit sjl;
    
    public float strength;
    public float curx;
    public float cury;
    public float curz;

    void Start() {
        rb = GetComponent<Rigidbody>();
        j = rb.GetComponent<ConfigurableJoint>();
        sjl = new SoftJointLimit();
        sjl.limit = 90*ROM;
        j.angularYLimit = sjl;
        j.angularZLimit = sjl;
        frontStopOutside = 270 + (90 - ROM * 90);
        frontStopInside = frontStopOutside + variance * ROM;
        backStopOutside = ROM * 90;
        backStopInside = backStopOutside - variance * ROM;
        topStopOutside = ROM * 90;
        topStopInside = topStopOutside - variance * ROM;
        bottomStopOutside = 270 + 90-(ROM * 90);
        bottomStopInside = frontStopOutside + variance * ROM;

    }
    // Update is called once per frame
    void FixedUpdate () {        
		if(MovingForward)
        {
            rb.AddTorque(strength * (new Vector3(0, -1f, 0)));
        }else
        {
            rb.AddTorque(strength * (new Vector3(0, 1f, 0)));
        }

        // Check if we are in the range for the backstop
        if(rb.transform.eulerAngles.y > (backStopInside) && rb.transform.eulerAngles.y < (backStopOutside))
        {
            MovingForward = true;
        }else
        if (rb.transform.eulerAngles.y > (frontStopOutside) && rb.transform.eulerAngles.y < (frontStopInside))
        {
            MovingForward = false;
        }

        
        if (MovingUp)
        {
            rb.AddTorque(strength * (new Vector3(0, 0, 1)));
        }
        else
        {
            rb.AddTorque(strength * (new Vector3(0, 0, -1)));
        }

        if (rb.transform.eulerAngles.z < (bottomStopInside) && rb.transform.eulerAngles.z > (bottomStopOutside))
        {
            MovingUp = true;
        }else
        if (rb.transform.eulerAngles.z > (topStopInside) && rb.transform.eulerAngles.z < (topStopOutside))
        {
            MovingUp = false;
        }
        curx = rb.transform.eulerAngles.x;
        cury = rb.transform.eulerAngles.y;
        curz = rb.transform.eulerAngles.z;
    }
}
