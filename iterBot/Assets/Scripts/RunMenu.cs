using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RunMenu : MonoBehaviour {
    [Header("RunTimeConstants")]
    public GameObject RunController;

    [Header("Cameras")]
    public Camera Camera1;
    public Camera Camera2;
    public Camera Camera3;

    [Header("Panels")]
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
    private bool paused = false;
    private int lastSelectedSpeedValue;
    public Button RestartButton;
    public Text RestartButtonText;

    public Slider XSizeSlider;
    public Slider YSizeSlider;
    public Slider ZSizeSlider;
    public Slider BlockDensitySlider;
    public Slider BlockStabilitySlider;
    public Dropdown BodyTypeDropdown;
    
    public Dropdown TerrainTypeDropdown;
    public Slider GravitationalForceSlider;

    public Dropdown NumberOfCreatures;
    public Slider TestingTimeSlider;

    public Slider XFitnessVectorSlider;
    public Slider YFitnessVectorSlider;

    public Text TipText;
    public Text AlertText;

    public GameObject FitnessTargetMarker;
    private GameObject Target;

    public Slider SimSpeedSlider;

    [Header("Coloring Options")]
    public Color PanelColor;
    public Color HeaviestBlockColor;
    public Color LightestBlockColor;
    void Start() { 
        lastSelectedSpeedValue = 1;

        ToolTipPanel.GetComponent<Image>().color = PanelColor;
        InfoPanel.GetComponent<Image>().color = PanelColor;
        ControlPanel.GetComponent<Image>().color = PanelColor;
        CameraPanel.GetComponent<Image>().color = PanelColor;
        SimulationSettingsPanel.GetComponent<Image>().color = PanelColor;

        ToolTipPanel.GetComponent<Image>().enabled = false;


        Target = Instantiate(FitnessTargetMarker, new Vector3(20, 0, 20), Quaternion.identity);
        XFitnessVectorChange();
        YFitnessVectorChange();


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
    void UpdateFitnessMarker() {
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
    public void TipTextTestingTimeSlider() {
        EnableTipTextBackground();
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

    public void StartButtonClick() {
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
}
