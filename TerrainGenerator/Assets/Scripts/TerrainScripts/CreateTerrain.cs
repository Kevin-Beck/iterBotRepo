using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTerrain : MonoBehaviour {
    private static CreateTerrain _instance;
    public static CreateTerrain Instance { get { return _instance; } }

    // Variables related to the data stored by the terrain class
    public setTerrainBars setMenu; 

    private int StructureType;
    private int TextureType;
    private float TextureDensity;
    private float TextureSize;
    private int ObstacleType;
    private float ObstacleDensity;
    private float ObstacleSize;
    private float Gravity;
    private int ExternalForceType;
    private float ExternalForceStrength;
    //Setters
    public void setStructureType(int newStructureType) { // This Structure type will be defined as a member of a list, generally 0-3 or something
        StructureType = newStructureType;
    }
    public void setTextureType(int newTextureType) { // This type will be defined as 0-4 or similar
        TextureType = newTextureType;
    }
    public void setTextureDensity(float newTextureDensity) { // This value will be stored as a decimal between 0-1.  to be controlled by public boundary variables
        TextureDensity = newTextureDensity;
    }
    public void setTextureSize(float newTextureSize) {// This value will be stored as a decimal between 0-1.  to be controlled by public boundary variables
        TextureSize = newTextureSize;
    }
    public void setObstacleType(int newObstacleType) {// This type will be defined as 0-4 or similar
        ObstacleType = newObstacleType;
    }
    public void setObstacleDensity(float newObstacleDensity) {// This value will be stored as a decimal between 0-1.  to be controlled by public boundary variables
        ObstacleDensity = newObstacleDensity;
    }
    public void setObstacleSize(float newObstacleSize) {// This value will be stored as a decimal between 0-1.  to be controlled by public boundary variables
        ObstacleSize = newObstacleSize;
    }
    public void setGravity(float newGravity) {
        Gravity = newGravity;
    }
    public void setExternalForceType(int newExternalForceType) {// This type will be defined as 0-4 or similar
        ExternalForceType = newExternalForceType;
    }
    public void setExternalForceStrength(float newExternalForceStrength) {// This value will be stored as a decimal between 0-1.  to be controlled by public boundary variables
        ExternalForceStrength = newExternalForceStrength;
    }
    //Getters
    public int getStructureType() {
        return StructureType;
    }
    public int getTextureType(){
        return TextureType;
    }
    public float getTextureDensity() {
        return TextureDensity;
    }
    public float getTextureSize() {
        return TextureSize;
    }
    public int getObstacleType() {
        return ObstacleType;
    }
    public float getObstacleDensity() {
        return ObstacleDensity;
    }
    public float getObstacleSize() {
        return ObstacleSize;
    }
    public float getGravity() {
        return Gravity;
    }
    public int getExternalForceType() {
        return ExternalForceType;
    }
    public float getExternalForceTypeStrength() {
        return ExternalForceStrength;
    }
    
    public int NumberOfStructureTypes;

    public int NumberOfTextureTypes;
    public GameObject TextureType0;
    public GameObject TextureType1;
    public GameObject TextureType2;
    public float TextureDensityMin;
    public float TextureDensityMax;
    public float TextureSizeMin;
    public float TextureSizeMax;
    public int NumberOfObstacleTypes;
    public GameObject ObstacleType0;
    public GameObject ObstacleType1;
    public GameObject ObstacleType2;
    public float ObstacleDensityMin;
    public float ObstacleDensityMax;
    public float ObstacleSizeMin;
    public float ObstacleSizeMax;
    public float GravityMin;
    public float GravityMax;
    public int NumberOfExternalForceTypes;
    public float ExternalForceStrengthMin;
    public float ExternalForceStrengthMax;

    // Variables used for the actual physical terrain
    private GameObject emptyFoundationParent;
    private GameObject foundation;
    public float foundationScaleX, foundationScaleZ;
    public void Awake() {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void Start()
    {
        setMenu.RandomizeButtonClick();
    }
    public void RandomizeTerrain() {
        // Randomizes all values of the terrain
        // The types are randomized, from 0 to 1 less than the number of types because the Max is exclusive
        // The other values are randomized between min and max as specified in the Editor
        StructureType = Random.Range(0, NumberOfStructureTypes);
        TextureType = Random.Range(0, NumberOfTextureTypes);
        TextureDensity = Random.Range((int)TextureDensityMin, (int)TextureDensityMax);
        TextureSize = Random.Range(TextureSizeMin, TextureSizeMax);
        ObstacleType = Random.Range(0, NumberOfObstacleTypes);
        ObstacleDensity = Random.Range((int)ObstacleDensityMin, (int)ObstacleDensityMax);
        ObstacleSize = Random.Range(ObstacleSizeMin, ObstacleSizeMax);
        Gravity = Random.Range(GravityMin, GravityMax);
        ExternalForceType = Random.Range(0, NumberOfExternalForceTypes);
        ExternalForceStrength = Random.Range(ExternalForceStrengthMin, ExternalForceStrengthMax);       
    }
    public void CreateTerrainFoundation(int foundationType) { // creates the foundation, modifies the private gameObject foundation
        emptyFoundationParent = new GameObject();
        emptyFoundationParent.transform.position = Vector3.zero;
        
        foundation = GameObject.CreatePrimitive(PrimitiveType.Plane);
        foundation.transform.position = Vector3.zero;
        foundation.transform.parent = emptyFoundationParent.transform;
        //foundation.transform.localScale += new Vector3(foundationScaleX, 0, foundationScaleZ);        
    }
    public void CreateTerrainTexture(int textType) {
        float fieldxmin, fieldxmax, fieldzmin, fieldzmax; // creates the zone for generating texture
        fieldxmin = foundationScaleX * -5.0f;
        fieldxmax = foundationScaleX * 5.0f;
        fieldzmin = foundationScaleZ * -5.0f;
        fieldzmax = foundationScaleZ * 5.0f;

        GameObject typeOfObject = null;
        
        switch(textType)
        {
            case 0: 
                typeOfObject = null;
                break;
            case 1: typeOfObject = TextureType0;
                break;
            case 2:
                typeOfObject = TextureType1;
                break;
            case 3:
                typeOfObject = TextureType2;
                break;
            default:
                Debug.Log("SwitchCaseFallThrough in CreateTerrain: CreateTerrainTexture");
                break;
        }
        if (typeOfObject != null)
        {
            for (int i = 0; i < TextureDensity; i++)
            {
                GameObject t = Instantiate(typeOfObject, new Vector3(Random.Range(fieldxmin, fieldxmax), -.2f*TextureSize, Random.Range(fieldzmin, fieldzmax)), new Quaternion(0, 0, 0, 0)) as GameObject;
                
                t.transform.localScale = new Vector3(TextureSize, TextureSize, TextureSize);
                t.transform.eulerAngles = new Vector3(45, Random.Range(0, 180), 45);
                t.transform.parent = foundation.transform;

            }
        }
        // After creating the texture for the surface, it tilts the surface to match the hill orientation
        switch (StructureType)
        {
            case 0:
                break;
            case 1:
                foundation.transform.eulerAngles = new Vector3(0, 0, 10);
                break;
            case 2:
                foundation.transform.eulerAngles = new Vector3(0, 0, 20);
                break;
            case 3:
                foundation.transform.eulerAngles = new Vector3(0, 0, 45);
                break;
            case 4:
                foundation.transform.eulerAngles = new Vector3(0, 0, 60);
                break;
            case 5:
                Debug.Log("Case 5 for creating the foundation"); // This will be where the stair construction is created. perhaps a loop of creating stair planes and putting them into the scene individually?
                break;
            default:
                Debug.Log("SwitchCaseFallThrough in CreateTerrain Foundation Type switch in CreateFoundation");
                break;
        }
    }
    public void CreateTerrainObstacles(int obsType) {
        float fieldxmin, fieldxmax, fieldzmin, fieldzmax; // creates the zone for generating texture
        fieldxmin = foundationScaleX * -3.5f;
        fieldxmax = foundationScaleX * 3.5f;
        fieldzmin = foundationScaleZ * -3.5f;
        fieldzmax = foundationScaleZ * 3.5f;

        GameObject typeOfObject = null;

        switch (obsType)
        {
            case 0:
                typeOfObject = null;
                break;
            case 1:
                typeOfObject = ObstacleType0;
                break;
            case 2:
                typeOfObject = ObstacleType1;
                break;
            case 3:
                typeOfObject = ObstacleType2;
                break;
            default:
                Debug.Log("SwitchCaseFallThrough in CreateTerrain: CreateTerrainTexture");
                break;
        }
        if (typeOfObject != null)
        {
            for (int i = 0; i < ObstacleDensity; i++)
            {
                GameObject t = Instantiate(typeOfObject, new Vector3(Random.Range(fieldxmin, fieldxmax), Random.Range(5, 10), Random.Range(fieldzmin, fieldzmax)), new Quaternion(0, 0, 0, 0)) as GameObject;

                t.transform.localScale = new Vector3(ObstacleSize, ObstacleSize, ObstacleSize);
                t.transform.eulerAngles = new Vector3(45, Random.Range(0, 180), 45);
                t.transform.parent = foundation.transform;
                t.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous; // change when you switch plane to blocks
            }
        }
    }
    public void AdjustGravity(float gravity) {
        Physics.gravity = new Vector3(0, -Gravity, 0);
    }
    public void GenerateTerrain () {
        DestroyTerrain();// Remove currently existing terrain, if any
        CreateTerrainFoundation(StructureType); // create the foundation
        CreateTerrainTexture(TextureType); // create the texture on the foundation
        CreateTerrainObstacles(ObstacleType);
        AdjustGravity(Gravity);
	}
    public void UpdateButtonClick() {
        GenerateTerrain();
    }
    public void RandomizeButtonClick() {
        RandomizeTerrain();
        setMenu.UpdateMenuValues();
        GenerateTerrain(); // finishes by creating the terrain
    }
	public void DestroyTerrain()
    {
        Destroy(emptyFoundationParent);
    }
}
