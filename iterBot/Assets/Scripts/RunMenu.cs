using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RunMenu : MonoBehaviour {
    [Header("RunTimeConstants")]
    public GameObject RunController;

    [Header("TextElements")]
    public int TextSize;
    public Text IndividualSizeText;
    public Text IndividualSizeXText;
    public Text IndividualSizeYText;
    public Text IndividualSizeZText;
    public Text BlockDensityText;
    public Text BlockStabilityText;
    public Text BodyCompositionText;
    public Text BodyDropDownText;
    public Text TerrainTypeText;
    public Text TerrainDropDownText;
    public Text GravitationalForceText;
    public Text CreaturesPerGenText;
    public Text CreaturesPerGenDropDownText;
    public Text TestingTimePerGenText;
    public Text FitnessVectorText;
    public Text FitnessVectorXText;
    public Text FitnessVectorYText;
    public Text SimulationSpeedText;

    [Header("ConfigMenu")]
    public Button ConfigMenuButton;
    public GameObject ConfigMenu;
    public Button ConfigDoneButton;
    public Button ConfigWindowButton;
    public Button ConfigFullScreenButton;
    public Dropdown ResolutionDropdown;
    private bool fullscreenbool = true;
    public Slider FontSizeSlider;

    [Header("Cameras")]
    public Camera Camera1;
    public Camera Camera2;
    public Camera Camera3;

    [Header("Main Panels")]
    public GameObject ToolTipPanel;
    public GameObject InfoPanel;
    public GameObject ControlPanel;
    public GameObject CameraPanel;
    public GameObject SimulationSettingsPanel;
    public GameObject AlertPanel;

    [Header("Cameras")]
    public Button Cam1Button;
    public Button Cam2Button;
    public Button Cam3Button;

    [Header("Top Info Stuff")]
    public Text generationText;
    public Text winnerFitnessText;
    public Text clockText;

    [Header("Control Stuff")]
    [Header("Individual")]
    public Slider XSizeSlider;
    public Slider YSizeSlider;
    public Slider ZSizeSlider;
    public Slider BlockDensitySlider;
    public Slider BlockStabilitySlider;
    public Dropdown BodyTypeDropdown;

    [Header("Environment")]
    public Dropdown TerrainTypeDropdown;
    public Slider GravitationalForceSlider;
    public GameObject TerrainPreview;

    [Header("Algorithm")]
    public Dropdown NumberOfCreatures;
    public Slider TestingTimeSlider;
    public Slider XFitnessVectorSlider;
    public Slider YFitnessVectorSlider;
    public GameObject FitnessTargetMarker;
    private GameObject Target;

    [Header("Start/Restart/Speed")]
    public Button RestartButton;
    public Text RestartButtonText;
    public Slider SimSpeedSlider;

    [Header("Information")]
    public Text TipText;
    public Text AlertText;

    [Header("Coloring Options")]
    public Color PanelColor;
    public Color HeaviestBlockColor;
    public Color LightestBlockColor;


    void Start() { 
        ToolTipPanel.GetComponent<Image>().color = PanelColor;
        InfoPanel.GetComponent<Image>().color = PanelColor;
        ControlPanel.GetComponent<Image>().color = PanelColor;
        CameraPanel.GetComponent<Image>().color = PanelColor;
        SimulationSettingsPanel.GetComponent<Image>().color = PanelColor;

        ToolTipPanel.GetComponent<Image>().enabled = false;

        Target = Instantiate(FitnessTargetMarker, new Vector3(20, 0, 20), Quaternion.identity);
        XFitnessVectorChange();
        YFitnessVectorChange();

        GenerateTerrainPreview();
}
    public void EnableTipTextBackground() {
        ToolTipPanel.GetComponent<Image>().enabled = true;
    }
    public void DisableTipTextBackground() {
        ToolTipPanel.GetComponent<Image>().enabled = false;
    }
    public void XFitnessVectorChange() {
        EnableTipTextBackground();
        TipText.text = "Current value: " + XFitnessVectorSlider.value.ToString("F");
        UpdateFitnessMarker();
    }
    public void YFitnessVectorChange() {
        EnableTipTextBackground();
        TipText.text = "Current value: " + YFitnessVectorSlider.value.ToString("F");
        UpdateFitnessMarker();
    }
    public void UpdateFitnessMarker() {
        Target.GetComponent<Transform>().position = new Vector3(175f, 25*YFitnessVectorSlider.value, -50 * XFitnessVectorSlider.value);
        RunController.GetComponent<Run>().fitnessVector = Target.GetComponent<Transform>().position;
    }
    public void TipTextXSliderValue() {
        EnableTipTextBackground();
        TipText.text = "Current value: " + XSizeSlider.value;
    }
    public void TipTextSimSpeedSliderValue() {
        EnableTipTextBackground();
        TipText.text = "Current value: " + SimSpeedSlider.value +"x";
    }
    public void TipTextYSliderValue() {
        EnableTipTextBackground();
        TipText.text = "Current value: " + YSizeSlider.value;
    }
    public void TipTextZSliderValue() {
        EnableTipTextBackground();
        TipText.text = "Current value: " + ZSizeSlider.value;
    }
    public void TipTextBlockDensitySlider() {
        EnableTipTextBackground();
        TipText.text = "Current value: " + BlockDensitySlider.value.ToString("F");
    }
    public void TipTextBlockStabilitySlider() {
        EnableTipTextBackground();
        TipText.text = "Current value: " + BlockStabilitySlider.value.ToString("F");
    }
    public void TipTextGravitationalForceSlider() {
        EnableTipTextBackground();
        TipText.text = "Current value: " + GravitationalForceSlider.value.ToString("F");
    }  
    public void SetTextSize() {
        TextSize = (int)FontSizeSlider.value;
        IndividualSizeText.fontSize = TextSize;
        IndividualSizeXText.fontSize = TextSize;
        IndividualSizeYText.fontSize = TextSize;
        IndividualSizeZText.fontSize = TextSize;
        BlockDensityText.fontSize = TextSize;
        BlockStabilityText.fontSize = TextSize;
        BodyCompositionText.fontSize = TextSize;
        BodyDropDownText.fontSize = TextSize - 3;
        TerrainTypeText.fontSize = TextSize;
        TerrainDropDownText.fontSize = TextSize - 3;
        GravitationalForceText.fontSize = TextSize;
        CreaturesPerGenText.fontSize = TextSize;
        CreaturesPerGenDropDownText.fontSize = TextSize - 3;
        TestingTimePerGenText.fontSize = TextSize;
        FitnessVectorText.fontSize = TextSize;
        FitnessVectorXText.fontSize = TextSize;
        FitnessVectorYText.fontSize = TextSize;
        SimulationSpeedText.fontSize = TextSize;
}
    public void TipTextTestingTimeSlider() {
        EnableTipTextBackground();
        RunController.GetComponent<Run>().delay = (int)TestingTimeSlider.value;
        TipText.text = "Current value: " + TestingTimeSlider.value;
    }
    public void SwitchSceneToMainMenu() {
        SceneManager.LoadScene(0);
    }
    public void SetSimulationData() {
        RunController.GetComponent<Run>().sizeOfCreaturesX = (int)XSizeSlider.value;
        RunController.GetComponent<Run>().sizeOfCreaturesY = (int)YSizeSlider.value;
        RunController.GetComponent<Run>().sizeOfCreaturesZ = (int)ZSizeSlider.value;
        RunController.GetComponent<Run>().densityOfBlocks = BlockDensitySlider.value;
        RunController.GetComponent<Run>().stabilizationChance = BlockStabilitySlider.value;
        RunController.GetComponent<Run>().SelectedSurfaceType = TerrainTypeDropdown.value;
        RunController.GetComponent<Run>().gravitationalForce = GravitationalForceSlider.value;
        RunController.GetComponent<Run>().selectedBodyType = BodyTypeDropdown.value;
        RunController.GetComponent<Run>().RowsOfCreatures = (NumberOfCreatures.value + 3);
        RunController.GetComponent<Run>().delay = (int)TestingTimeSlider.value;
    }
    public void SpeedSelection() {
        RunController.GetComponent<Run>().UpdateSimulationSpeed();
    }
    public void GenerateTerrainPreview() {
        if (generationText.text == "Generation:")
        {
            try
            {
                GameObject.Destroy(GameObject.Find("Terrain0"));
            }
            catch
            {
            }
            RunController.GetComponent<Run>().SelectedSurfaceType = TerrainTypeDropdown.value;
            RunController.GetComponent<Run>().GenerateTerrainSurface(Vector3.zero, 0);
        }        
    }
    public void StartButtonClick() {
        try
        {
            if(generationText.text == "Generation:")
            {
                GameObject.Destroy(GameObject.Find("Terrain0"));
            }
        }catch
        {
        }
        if (generationText.text != "Generation: 0")
        {
            SetSimulationData();
            RestartButtonText.text = "Restart";        
            UpdateWinnerFitnessText(0);
            RunController.GetComponent<Run>().RestartSimulation();
            AlertText.text = "Initializing base model for simulation.";
        }else
        {
            AlertText.text = "Cannot restart until initialization procedure has finished.";
        }
    }
    public void SwitchToCam1() {
        Camera1.enabled = true;
        Camera2.enabled = false;
        Camera3.enabled = false;
    }
    public void SwitchToCam2() {
        Camera2.enabled = true;
        Camera1.enabled = false;
        Camera3.enabled = false;
    }
    public void SwitchToCam3() {
        Camera3.enabled = true;
        Camera1.enabled = false;
        Camera2.enabled = false;
    }
    public float GetSimulationSpeed() {
        return SimSpeedSlider.value;
    }
    public void UpdateGenerationText(float value) {
        generationText.text = "Generation: " + value;
    }
    public void UpdateWinnerFitnessText(float fitness) {
        winnerFitnessText.text = "Winner Fitness: " + (int)fitness;
    }
    public void UpdateClockText(float clock) {
        
        clockText.text = "Time: " + clock.ToString("F");
    }
    public void ConfigButtonClick() {
        ConfigMenu.SetActive(true);
    }
    public void ConfigDoneButtonClick() {
        ConfigMenu.SetActive(false);
    }
    public void FullScreenButtonPress() {
        fullscreenbool = true;
        ResolutionChanged();
    }
    public void WindowButtonPress() {
        fullscreenbool = false;
        ResolutionChanged();
    }
    public void ResolutionChanged() {
        int dropdownvalue = ResolutionDropdown.value;
        if (dropdownvalue == 0)
        {
            Screen.SetResolution(1280, 720, fullscreenbool);
        }
        else if(dropdownvalue == 1)
        {
            Screen.SetResolution(1366, 768, fullscreenbool);
        }
        else if(dropdownvalue == 2)
        {
            Screen.SetResolution(1920, 1080, fullscreenbool);
        }
        else if(dropdownvalue == 3)
        {
            Screen.SetResolution(2560, 1440, fullscreenbool);
        }
       
    }
}
