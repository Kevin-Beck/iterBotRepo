using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunMenu : MonoBehaviour {
    public GameObject RunController;

    public Camera Camera1;
    public Camera Camera2;
    public Camera Camera3;

    public Dropdown timeScaleDropdown;

    public Text generationText;
    public Text winnerFitnessText;
    public Text clockText;

    public Button Cam1Button;
    public Button Cam2Button;
    public Button Cam3Button;

    public Button PauseButton;
    public Text PauseButtonText;
    public bool paused = false;
    public int lastSelectedSpeedValue;

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

    public Text TipText;


    public void TipTextXSliderValue() {
        TipText.text = "Current value: " + XSizeSlider.value;
    }
    public void TipTextYSliderValue() {
        TipText.text = "Current value: " + YSizeSlider.value;
    }
    public void TipTextZSliderValue() {
        TipText.text = "Current value: " + ZSizeSlider.value;
    }
    public void TipTextBlockDensitySlider() {
        TipText.text = "Current value: " + BlockDensitySlider.value.ToString("F");
    }
    public void TipTextBlockStabilitySlider() {
        TipText.text = "Current value: " + BlockStabilitySlider.value.ToString("F");
    }
    public void TipTextGravitationalForceSlider() {
        TipText.text = "Current value: " + GravitationalForceSlider.value.ToString("F");
    }
    public void TipTextTestingTimeSlider() {
        TipText.text = "Current value: " + TestingTimeSlider.value;
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
        if(timeScaleDropdown.value != 0)
        {
            lastSelectedSpeedValue = timeScaleDropdown.value;
        }
        RunController.GetComponent<Run>().UpdateSimulationSpeed();
    }
    public void PauseButtonClick() {
        if (PauseButtonText.text == "Pause")
        {
            paused = true;
            PauseButtonText.text = "Run";
            timeScaleDropdown.value = 0;
            RunController.GetComponent<Run>().UpdateSimulationSpeed();
        }
        else
        {
            paused = false;
            PauseButtonText.text = "Pause";
            timeScaleDropdown.value = lastSelectedSpeedValue;
            RunController.GetComponent<Run>().UpdateSimulationSpeed();
        }
    }
    public void StartButtonClick() {
        SetSimulationData();
        RestartButtonText.text = "Restart";
        RunController.GetComponent<Run>().RestartSimulation();
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
    public int GetSimulationSpeed() {
        return timeScaleDropdown.value;
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

    // Use this for initialization
    void Start () {
        lastSelectedSpeedValue = 1;
	}
}
