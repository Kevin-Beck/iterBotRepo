using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MaxBarScript : MonoBehaviour {
    public Slider Max;
    public Slider Min;

    public void CheckOtherSlider() {
        if(Min.value > Max.value)
        {
            Min.value = Max.value;
        }
    }
}
