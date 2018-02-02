using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setRobotBars : MonoBehaviour {
    public RobotSettingsController Controller;

    public Slider segmentsMinSlider;
    public Slider segmentsMaxSlider;
    public Slider weightMinSlider;
    public Slider weightMaxSlider;
    public Slider upperLengthMinSlider;
    public Slider upperLengthMaxSlider;
    public Slider upperStrengthMinSlider;
    public Slider upperStrengthMaxSlider;
    public Slider upperRomMinSlider;
    public Slider upperRomMaxSlider;
    public Slider lowerLengthMinSlider;
    public Slider lowerLengthMaxSlider;
    public Slider lowerStrengthMinSlider;
    public Slider lowerStrengthMaxSlider;
    public Slider lowerRomMinSlider;
    public Slider lowerRomMaxSlider;
    public Slider carryingWeightSlider;
    public Slider dragWeightSlider;
    public Button updateButton;

    // Use this for initialization
    private void Awake() {
        Controller = FindObjectOfType<RobotSettingsController>(); // If this is a newly created menu item, the menu needs to go find the controller
        
    }
    void Start () {
        InitializeMenuItems();
        updateButton.onClick.AddListener(SendValuesToController);
        UpdateMenuBarValues();
    }
    void InitializeMenuItems() {
        // These have to set the min and max values for the menu bars for the robot scene
        // So each one of these says Menu bar min value = whatever, its max value = whatever, and
        // then the same thing for its max bar
        segmentsMinSlider.minValue = Controller.segmentBarsMin;
        segmentsMinSlider.maxValue = Controller.segmentBarsMax;
        segmentsMaxSlider.minValue = Controller.segmentBarsMin;
        segmentsMaxSlider.maxValue = Controller.segmentBarsMax;
        weightMinSlider.minValue = Controller.weightBarsMin;
        weightMinSlider.maxValue = Controller.weightBarsMax;
        weightMaxSlider.minValue = Controller.weightBarsMin;
        weightMaxSlider.maxValue = Controller.weightBarsMax;
        upperLengthMinSlider.minValue = Controller.upperLengthBarsMin;
        upperLengthMinSlider.maxValue = Controller.upperLengthBarsMax;
        upperLengthMaxSlider.minValue = Controller.upperLengthBarsMin;
        upperLengthMaxSlider.maxValue = Controller.upperLengthBarsMax;
        upperStrengthMinSlider.minValue = Controller.upperStrengthBarsMin;
        upperStrengthMinSlider.maxValue = Controller.upperStrengthBarsMax;
        upperStrengthMaxSlider.minValue = Controller.upperStrengthBarsMin;
        upperStrengthMaxSlider.maxValue = Controller.upperStrengthBarsMax;
        upperRomMinSlider.minValue = Controller.upperROMBarsMin;
        upperRomMinSlider.maxValue = Controller.upperROMBarsMax;
        upperRomMaxSlider.minValue = Controller.upperROMBarsMin;
        upperRomMaxSlider.maxValue = Controller.upperROMBarsMax;
        lowerLengthMinSlider.minValue = Controller.lowerLengthBarsMin;
        lowerLengthMinSlider.maxValue = Controller.lowerLengthBarsMax;
        lowerLengthMaxSlider.minValue = Controller.lowerLengthBarsMin;
        lowerLengthMaxSlider.maxValue = Controller.lowerLengthBarsMax;
        lowerStrengthMinSlider.minValue = Controller.lowerStrengthBarsMin;
        lowerStrengthMinSlider.maxValue = Controller.lowerStrengthBarsMax;
        lowerStrengthMaxSlider.minValue = Controller.lowerStrengthBarsMin;
        lowerStrengthMaxSlider.maxValue = Controller.lowerStrengthBarsMax;
        lowerRomMinSlider.minValue = Controller.lowerROMBarsMin;
        lowerRomMinSlider.maxValue = Controller.lowerROMBarsMax;
        lowerRomMaxSlider.minValue = Controller.lowerROMBarsMin;
        lowerRomMaxSlider.maxValue = Controller.lowerROMBarsMax;
        carryingWeightSlider.minValue = Controller.carryingWeightMin;
        carryingWeightSlider.maxValue = Controller.carryingWeightMax;
        dragWeightSlider.minValue = Controller.dragWeightMin;
        dragWeightSlider.maxValue = Controller.dragWeightMax;

        //Set the Values of the thumbs
        segmentsMinSlider.value = Controller.GetNumBodySegmentsMin();
        segmentsMaxSlider.value = Controller.GetNumBodySegmentsMax();
        weightMinSlider.value = Controller.GetBodyWeightMin();
        weightMaxSlider.value = Controller.GetBodyWeightMax();
        upperLengthMinSlider.value = Controller.GetUpperLengthMin();
        upperLengthMaxSlider.value = Controller.GetUpperLengthMax();
        upperStrengthMinSlider.value = Controller.GetUpperStrengthMin();
        upperStrengthMaxSlider.value = Controller.GetUpperStrengthMax();
        upperRomMinSlider.value = Controller.GetUpperROMMin();
        upperRomMaxSlider.value = Controller.GetUpperROMMax();
        lowerLengthMinSlider.value = Controller.GetLowerLengthMin();
        lowerLengthMaxSlider.value = Controller.GetLowerLengthMax();
        lowerStrengthMinSlider.value = Controller.GetLowerStrengthMin();
        lowerStrengthMaxSlider.value = Controller.GetLowerStrengthMax();
        lowerRomMinSlider.value = Controller.GetLowerROMMin();
        lowerRomMaxSlider.value = Controller.GetLowerROMMax();



    }
    void UpdateMenuBarValues() {
        segmentsMinSlider.value = Controller.GetNumBodySegmentsMin();
        segmentsMaxSlider.value = Controller.GetNumBodySegmentsMax();
        weightMinSlider.value = Controller.GetBodyWeightMin();
        weightMaxSlider.value = Controller.GetBodyWeightMax();
        upperLengthMinSlider.value = Controller.GetUpperLengthMin();
        upperLengthMaxSlider.value = Controller.GetUpperLengthMax();
        upperStrengthMinSlider.value = Controller.GetUpperStrengthMin();
        upperStrengthMaxSlider.value = Controller.GetUpperStrengthMax();
        upperRomMinSlider.value = Controller.GetUpperROMMin();
        upperRomMaxSlider.value = Controller.GetUpperROMMax();
        lowerLengthMinSlider.value = Controller.GetLowerLengthMin();
        lowerLengthMaxSlider.value = Controller.GetLowerLengthMax();
        lowerStrengthMinSlider.value = Controller.GetLowerStrengthMin();
        lowerStrengthMaxSlider.value = Controller.GetLowerStrengthMax();
        lowerRomMinSlider.value = Controller.GetLowerROMMin();
        lowerRomMaxSlider.value = Controller.GetLowerROMMax();
        carryingWeightSlider.value = Controller.GetCarryingWeight();
        dragWeightSlider.value = Controller.GetDragWeight();
    }
    void SendValuesToController() {
        Debug.Log("ValuesSending");
        Debug.Log(segmentsMinSlider.value);
        Controller.SetNumBodySegmentsMin((int)segmentsMinSlider.value);
        Controller.SetNumBodySegmentsMax((int)segmentsMaxSlider.value);
        Controller.SetWeightMin(weightMinSlider.value);
        Controller.SetWeightMax(weightMaxSlider.value);
        Controller.SetUpperLengthMin(upperLengthMinSlider.value);
        Controller.SetUpperLengthMax(upperLengthMaxSlider.value);
        Controller.SetUpperStrengthMin(upperStrengthMinSlider.value);
        Controller.SetUpperStrengthMax(upperStrengthMaxSlider.value);
        Controller.SetUpperROMMin(upperRomMinSlider.value);
        Controller.SetUpperROMMax(upperRomMaxSlider.value);
        Controller.SetLowerLengthMin(lowerLengthMinSlider.value);
        Controller.SetLowerLengthMax(lowerLengthMaxSlider.value);
        Controller.SetLowerStrengthMin(lowerStrengthMinSlider.value);
        Controller.SetLowerStrengthMax(lowerStrengthMaxSlider.value);
        Controller.SetLowerROMMin(lowerRomMinSlider.value);
        Controller.SetLowerROMMax(lowerRomMaxSlider.value);
        Controller.SetCarryingWeight(carryingWeightSlider.value);
        Controller.SetDragWeight(dragWeightSlider.value);
    }
}
