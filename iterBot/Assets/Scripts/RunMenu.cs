using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunMenu : MonoBehaviour {

    public Dropdown timeScaleDropdown;

    public Text generationText;
    public Text winnerFitnessText;

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
