using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SelectAllOfTag : ScriptableWizard {

    public string searchTag = "Tag to find";

    [MenuItem("My Tools/Select All of Tag...")]
    static void SelectAllOfTagWizard() {
        ScriptableWizard.DisplayWizard<SelectAllOfTag>("Select All Of Tag...", "Make Selection");
    }
    private void OnWizardCreate() {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(searchTag);
        Selection.objects = gameObjects;
    }
}
