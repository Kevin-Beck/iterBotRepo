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

    public Button Cam1Button;
    public Button Cam2Button;
    public Button Cam3Button;

    public Button PauseButton;
    public Text PauseButtonText;
    private bool paused = false;
    private int lastSelectedSpeedValue;

    public Button RestartButton;
    public Text RestartButtonText;

    public void SpeedSelection() {
        lastSelectedSpeedValue = timeScaleDropdown.value;
        RunController.GetComponent<Run>().UpdateSimulationSpeed();
    }
    public void PauseButtonClick() {
        if (PauseButtonText.text == "Pause")
        {
            paused = true;
            PauseButtonText.text = "Run";
            lastSelectedSpeedValue = timeScaleDropdown.value;
            timeScaleDropdown.value = 0;
            RunController.GetComponent<Run>().UpdateSimulationSpeed();
        }
        else if (PauseButtonText.text == "Run") 
        {
            paused = false;
            PauseButtonText.text = "Pause";
            timeScaleDropdown.value = lastSelectedSpeedValue;
            RunController.GetComponent<Run>().UpdateSimulationSpeed();
        }
    }
    public void StartButtonClick() {
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

    // Use this for initialization
    void Start () {
        lastSelectedSpeedValue = 1;
	}
}
