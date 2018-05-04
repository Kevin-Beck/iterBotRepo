using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneMenu : MonoBehaviour {

    public GameObject FrontLeft;
    public GameObject FrontRight;
    public GameObject BackLeft;
    public GameObject BackRight;

    public Slider UpSlider;
    public Slider RotSlider;
    public Slider ForSlider;
    public Slider RightSlider;

    public float forstrength = 0.05f;
    public float rightstrength = 0.05f;
    public float rotstrength = 5f;

    public 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ForwardKey();
        BackwardKey();
        LeftKey();
        RightKey();
        RotRightKey();
        RotLeftKey();
        UpKey();
        DownKey();
        ZeroKeys();
	}
    public void ZeroKeys() {
        if (Input.GetKey("y"))
        {
            ForSlider.value = (ForSlider.maxValue + ForSlider.minValue) / 2;
            RotSlider.value = (RotSlider.maxValue + RotSlider.minValue) / 2;
            RightSlider.value = (RightSlider.maxValue + RightSlider.minValue) / 2;
            UpSlider.value = (UpSlider.maxValue + UpSlider.minValue) / 2;

            UpdateDroneFor();
            UpdateDroneRight();
            UpdateDroneRot();
            UpdateDroneUp();
        }
    }
    public void ForwardKey() {
        if (Input.GetKey("w"))
        {
            ForSlider.value = ForSlider.value + 0.1f; ;
            UpdateDroneFor();
            print("w");
        }
    }
    public void BackwardKey() {
        if (Input.GetKey("s"))
        {
            ForSlider.value = ForSlider.value - 0.1f;
            UpdateDroneFor();
            
        }
    }
    public void LeftKey() {
        if (Input.GetKey("a"))
        {
            RightSlider.value = RightSlider.value + 0.1f;
            UpdateDroneRight();
            print("a");
        }
    }
    public void RightKey() {
        if (Input.GetKey("d"))
        {
            RightSlider.value = RightSlider.value - 0.1f;
            UpdateDroneRight();
            print("d");
        }
    }
    public void UpKey() {
        if (Input.GetKey("u"))
        {
            UpSlider.value = UpSlider.value + 0.04f;
            UpdateDroneUp();
            print("u");
        }
    }
    public void DownKey() {
        if (Input.GetKey("j"))
        {
            UpSlider.value = UpSlider.value - 0.04f;
            UpdateDroneUp();
            print("j");
        }
    }
    public void RotLeftKey() {
        if (Input.GetKey("q"))
        {
            RotSlider.value = RotSlider.value - 1f;
            UpdateDroneRot();
            print("q");
        }
    }
    public void RotRightKey() {
        if (Input.GetKey("e"))
        {
            RotSlider.value = RotSlider.value + 1f;
            UpdateDroneRot();
            print("e");
        }
    }
    public void UpdateDroneUp() {
        FrontLeft.GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeForce = new Vector3(0, UpSlider.value, 0);
        FrontRight.GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeForce = new Vector3(0, UpSlider.value, 0);
        BackLeft.GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeForce = new Vector3(0, UpSlider.value, 0);
        BackRight.GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeForce = new Vector3(0, UpSlider.value, 0);
    }
    public void UpdateDroneRot() {
        FrontLeft.GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeTorque = new Vector3(0, RotSlider.value*rotstrength, 0);
        BackRight.GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeTorque = new Vector3(0, RotSlider.value*rotstrength, 0);
        FrontRight.GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeTorque = new Vector3(0, RotSlider.value*rotstrength, 0);
        BackLeft.GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeTorque = new Vector3(0, RotSlider.value*rotstrength, 0);
    }
    public void UpdateDroneFor() {
        FrontLeft.GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeForce = new Vector3(0, UpSlider.value-(ForSlider.value*forstrength), 0);
        FrontRight.GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeForce = new Vector3(0, UpSlider.value-(ForSlider.value*forstrength), 0);
        BackLeft.GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeForce = new Vector3(0, UpSlider.value+(ForSlider.value*forstrength), 0);
        BackRight.GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeForce = new Vector3(0, UpSlider.value+(ForSlider.value*forstrength), 0);
    }
    public void UpdateDroneRight() {
        FrontLeft.GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeForce = new Vector3(0, UpSlider.value - (RightSlider.value * rightstrength), 0);
        FrontRight.GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeForce = new Vector3(0, UpSlider.value + (RightSlider.value * rightstrength), 0);
        BackLeft.GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeForce = new Vector3(0, UpSlider.value - (RightSlider.value * rightstrength), 0);
        BackRight.GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeForce = new Vector3(0, UpSlider.value + (RightSlider.value * rightstrength), 0);
    }
}
