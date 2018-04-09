using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlideShow : MonoBehaviour {

    public Texture[] images = new Texture[5];
    public int currentImage;
    public Image displayImage;
    GUITexture curimagetexture;

    Rect imageRect;
    Rect buttonRect;
 
void Start() {
        currentImage = 0;
        curimagetexture = displayImage.GetComponent<GUITexture>();
        /*
        imageRect = new Rect(0, 0, Screen.width, Screen.height);
        buttonRect = new Rect(0, Screen.height - Screen.height / 10, Screen.width, Screen.height / 10);
        */

    }

    /*

    void OnGUI() {

        imageRect = new Rect(0, 0, Screen.width, Screen.height);
        GUI.Label(imageRect, images[currentImage]);
        if (GUI.Button(buttonRect, "Next"))
            currentImage++;
        if(currentImage > images.Length)
        {
            currentImage = 0;
        }
    }
    */
}
