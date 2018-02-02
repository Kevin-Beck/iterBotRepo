using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinBarScript : MonoBehaviour {
    public Slider Min;
    public Slider Max;
	// Use this for initialization
	public void CheckOther()
    {
        if(Min.value > Max.value)
        {
            Max.value = Min.value;
        }
    }
}
