using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;


public class DataManager : MonoBehaviour{
    public bool DataCollection = false;
    public int columnCount = 0;
    
    public void InitDB(string row1) {
        string[] headers = row1.Split(new string[] { "," },StringSplitOptions.None);
    }
    public void DeleteDB() {
        try
        {
            string path = Application.dataPath + "/CSV/" + "Saved_data.csv";
            File.Delete(path);
        }
        catch {
            Debug.Log("Error Deleting database in datamanager did not happen");
                };
    }
    public void SaveLine(string row) {
        StreamWriter outStream = new StreamWriter(getPath(), true);
        outStream.WriteLine(row);
        outStream.Close();

    }
    public void Save() {

    }
    private string getPath() {

#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + "Saved_data.csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
#else
        return Application.dataPath +"/"+"Saved_data.csv";
#endif
    }

}
