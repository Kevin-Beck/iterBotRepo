  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotSettingsController : MonoBehaviour {
    // Only want one of these, so we create instance and dont let it go
    private static RobotSettingsController _instance;
    public static RobotSettingsController Instance { get { return _instance; } }

    // Database Values for generating 
    private int numBodySegmentsMin;
    private int numBodySegmentsMax;
    private float weightMin;
    private float weightMax;
    private float upperLengthMin;
    private float upperLengthMax;
    private float upperStrengthMin;
    private float upperStrengthMax;
    private float upperROMMin;
    private float upperROMMax;
    private float lowerLengthMin;
    private float lowerLengthMax;
    private float lowerStrengthMin;
    private float lowerStrengthMax;
    private float lowerROMMin;
    private float lowerROMMax;
    private float carryingWeight;
    private float dragWeight;
    //Setters
    public void SetNumBodySegmentsMin(int min) {
        numBodySegmentsMin = min;    }
    public void SetNumBodySegmentsMax(int max) {
        numBodySegmentsMax = max;    }
    public void SetWeightMin(float min) {
        weightMin = min;    }
    public void SetWeightMax(float max) {
        weightMax = max;    }
    public void SetUpperLengthMin(float min) {
        upperLengthMin = min;
    }
    public void SetUpperLengthMax(float max) {
        upperLengthMax = max;
    }
    public void SetUpperStrengthMin(float min) {
        upperStrengthMin = min;
    }
    public void SetUpperStrengthMax(float max) {
        upperStrengthMax = max;
    }
	public void SetUpperROMMin(float min) {
        upperROMMin = min;
    }
    public void SetUpperROMMax(float max) {
        upperROMMax = max;
    }
    public void SetLowerLengthMin(float min) {
        lowerLengthMin = min;
    }
    public void SetLowerLengthMax(float max) {
        lowerLengthMax = max;
    }
    public void SetLowerStrengthMin(float min) {
        lowerStrengthMin = min;
    }
    public void SetLowerStrengthMax(float max) {
        lowerStrengthMax = max;
    }
    public void SetLowerROMMin(float min) {
        lowerROMMin = min;
    }
    public void SetLowerROMMax(float max) {
        lowerROMMax = max;
    }
    public void SetCarryingWeight(float weight) {
        carryingWeight = weight;
    }
    public void SetDragWeight(float weight) {
        dragWeight = weight;
    }
    // Getters
    public int GetNumBodySegmentsMin() {
        return numBodySegmentsMin;
    }
    public int GetNumBodySegmentsMax() {
        return numBodySegmentsMax;
    }
    public float GetBodyWeightMin() {
        return weightMin;
    }
    public float GetBodyWeightMax() {
        return weightMax;
    }
    public float GetUpperLengthMin() {
        return upperLengthMin;
    }
    public float GetUpperLengthMax() {
        return upperLengthMax;
    }
    public float GetUpperStrengthMin() {
        return upperStrengthMin;
    }
    public float GetUpperStrengthMax() {
        return upperStrengthMax;
    }
    public float GetUpperROMMin() {
        return upperROMMin;
    }
    public float GetUpperROMMax() {
        return upperROMMax;
    }
    public float GetLowerLengthMin() {
        return lowerLengthMin;
    }
    public float GetLowerLengthMax() {
        return lowerLengthMax;
    }
    public float GetLowerStrengthMin() {
        return lowerStrengthMin;
    }
    public float GetLowerStrengthMax() {
        return lowerStrengthMax;
    }
    public float GetLowerROMMin() {
        return lowerROMMin;
    }
    public float GetLowerROMMax() {
        return lowerROMMax;
    }
    public float GetCarryingWeight() {
        return carryingWeight;
    }
    public float GetDragWeight() {
        return dragWeight;
    }
    
    // Editor Variables for MenuValues
    public int segmentBarsMin;
    public int segmentBarsMax;
    public float weightBarsMin;
    public float weightBarsMax;
    public float upperLengthBarsMin;
    public float upperLengthBarsMax;
    public float upperStrengthBarsMin;
    public float upperStrengthBarsMax;
    public float upperROMBarsMin;
    public float upperROMBarsMax;
    public float lowerLengthBarsMin;
    public float lowerLengthBarsMax;
    public float lowerStrengthBarsMin;
    public float lowerStrengthBarsMax;
    public float lowerROMBarsMin;
    public float lowerROMBarsMax;
    public float carryingWeightMin;
    public float carryingWeightMax;
    public float dragWeightMin;
    public float dragWeightMax;

    public void Awake() {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	
}
