using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunMenu : MonoBehaviour {
    public Camera Camera1;
    public Camera Camera2;
    public Camera Camera3;

    public Dropdown timeScaleDropdown;

    public Text generationText;
    public Text winnerFitnessText;

    public Button Cam1Button;
    public Button Cam2Button;
    public Button Cam3Button;

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
        return timeScaleDropdown.value + 1;
    }
    public void UpdateGenerationText(float value) {
        generationText.text = "Generation: " + value;
    }
    public void UpdateWinnerFitnessText(float fitness) {
        winnerFitnessText.text = "Winner Fitness: " + (int)fitness;
    }

    // Use this for initialization
    void Start () {
		
	}
}
