using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setTerrainBars : MonoBehaviour {
    public CreateTerrain Controller;

    public Dropdown StructureDropDown;
    public Dropdown TextureDropDown;
    public Slider TextureDensitySlider;
    public Slider TextureSizeSlider;
    public Dropdown ObstacleDropDown;
    public Slider ObstacleDensitySlider;
    public Slider ObstacleSizeSlider;
    public Slider GravitySlider;
    public Dropdown ExternalForceDropDown;
    public Slider ExternalForceStrengthSlider;
    public Button UpdateButton;
    public Button RandomizeButton;
    
	// Use this for initialization
	void Start () {
        InitializeController(); // links the Terrain controller to this panel object so that the panel can interact with the controller
        InitializeButtons(); // Creates the buttons link to the controller functions that execute the "update" and "random" functions in controller
        UpdateMenuValues(); // When menu is created, the values are adjusted to reflect those stored in the Terrain Controller
        Controller.UpdateButtonClick(); // runs the update button once the scene is reestabolished, otherwise the scene will start empty

        //After scene is set:
        AddListenerstoMenuItems(); // This creates the listeners on each menu item that will allow the menu item to update the values in controller
	}
    private void AddListenerstoMenuItems() {
        StructureDropDown.onValueChanged.AddListener(delegate { SendControllerTerrainValues(); });
        TextureDropDown.onValueChanged.AddListener(delegate { SendControllerTerrainValues(); });
        TextureDensitySlider.onValueChanged.AddListener(delegate { SendControllerTerrainValues(); });
        TextureSizeSlider.onValueChanged.AddListener(delegate { SendControllerTerrainValues(); });
        ObstacleDropDown.onValueChanged.AddListener(delegate { SendControllerTerrainValues();  });
        ObstacleDensitySlider.onValueChanged.AddListener(delegate { SendControllerTerrainValues(); });
        ObstacleSizeSlider.onValueChanged.AddListener(delegate { SendControllerTerrainValues(); });
        GravitySlider.onValueChanged.AddListener(delegate { SendControllerTerrainValues(); });
        ExternalForceDropDown.onValueChanged.AddListener(delegate { SendControllerTerrainValues(); });
        ExternalForceStrengthSlider.onValueChanged.AddListener(delegate { SendControllerTerrainValues(); });
    }
    private void InitializeButtons() {
        UpdateButton.onClick.AddListener(UpdateButtonClick); // This estabolishes what will be called, which just passes the call to the controller to handle.
        RandomizeButton.onClick.AddListener(RandomizeButtonClick);// This estabolishes what will be called, which passes it to the controller to handle
    }
    private void InitializeController() {
        Controller = FindObjectOfType<CreateTerrain>(); // If this is a newly created menu item, the menu needs to go find the controller
        Controller.setMenu = this;
    }
    public void SendControllerTerrainValues() { 
        // This function updates the values of the CreateTerrain Class
        Controller.setStructureType(StructureDropDown.value);
        Controller.setTextureType(TextureDropDown.value);
        Controller.setTextureDensity(TextureDensitySlider.value);
        Controller.setTextureSize(TextureSizeSlider.value);
        Controller.setObstacleType(ObstacleDropDown.value);
        Controller.setObstacleDensity(ObstacleDensitySlider.value);
        Controller.setObstacleSize(ObstacleSizeSlider.value);
        Controller.setGravity(GravitySlider.value);
        Controller.setExternalForceType(ExternalForceDropDown.value);
        Controller.setExternalForceStrength(ExternalForceStrengthSlider.value);
    }
    public void UpdateButtonClick() {
        Controller.UpdateButtonClick();
    }
    public void RandomizeButtonClick() {
        RemoveListenersFromMenuItems(); //Randomizing the values causes the listeners to fire, thus we remove them
        Controller.RandomizeButtonClick(); // then we randomize the values.
        AddListenerstoMenuItems(); // then we add the listeners back in because we're clever
    }
    private void RemoveListenersFromMenuItems() { // removes all listeners from menu items.  Listeners need to be removed before manipulating the
        // values otherwise, it will trigger the listener after a script changes the values.
        StructureDropDown.onValueChanged.RemoveAllListeners();
        TextureDropDown.onValueChanged.RemoveAllListeners();
        TextureDensitySlider.onValueChanged.RemoveAllListeners();
        TextureSizeSlider.onValueChanged.RemoveAllListeners();
        ObstacleDensitySlider.onValueChanged.RemoveAllListeners();
        ObstacleSizeSlider.onValueChanged.RemoveAllListeners();
        ObstacleDropDown.onValueChanged.RemoveAllListeners();
        ExternalForceDropDown.onValueChanged.RemoveAllListeners();
        ExternalForceStrengthSlider.onValueChanged.RemoveAllListeners();
        GravitySlider.onValueChanged.RemoveAllListeners();
    }
    public void UpdateMenuValues() {
        // Update the menu values to reflect what the controller has for the min/max/value of all the bars and such
        StructureDropDown.value = Controller.getStructureType();
        TextureDropDown.value = Controller.getTextureType();
        TextureDensitySlider.minValue = Controller.TextureDensityMin;
        TextureDensitySlider.maxValue = Controller.TextureDensityMax;
        TextureDensitySlider.value = Controller.getTextureDensity();
        TextureSizeSlider.minValue = Controller.TextureSizeMin;
        TextureSizeSlider.maxValue = Controller.TextureSizeMax;
        TextureSizeSlider.value = Controller.getTextureSize();

        ObstacleDropDown.value = Controller.getObstacleType();
        ObstacleDensitySlider.minValue = Controller.ObstacleDensityMin;
        ObstacleDensitySlider.maxValue = Controller.ObstacleDensityMax;
        ObstacleDensitySlider.value = Controller.getObstacleDensity();
        ObstacleSizeSlider.minValue = Controller.ObstacleSizeMin;
        ObstacleSizeSlider.maxValue = Controller.ObstacleSizeMax;
        ObstacleSizeSlider.value = Controller.getObstacleSize();

        GravitySlider.minValue = Controller.GravityMin;
        GravitySlider.maxValue = Controller.GravityMax;
        GravitySlider.value = Controller.getGravity();

        ExternalForceDropDown.value = Controller.getExternalForceType();
        ExternalForceStrengthSlider.minValue = Controller.ExternalForceStrengthMin;
        ExternalForceStrengthSlider.maxValue = Controller.ExternalForceStrengthMax;
        ExternalForceStrengthSlider.value = Controller.getExternalForceTypeStrength();
    }
}
