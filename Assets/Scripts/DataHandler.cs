using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;


public class DataHandler : MonoBehaviour
{
    public void Start ()
    {
        LoadData();
    }

    [SerializeField] 
    string filePath = "Assets/Scripts/level_data.json";

    private LevelData levelData;


    private void LoadData()
    {
        Debug.Log("Before starting");
        using (StreamReader reader = new StreamReader(filePath))
        {
            string json = reader.ReadToEnd();
            levelData = JsonUtility.FromJson<LevelData>(json);
        }
        UnityEngine.Debug.Log(" The length of the data " + levelData.levels.Length);
        UnityEngine.Debug.Log(" data " + levelData.levels[0].level_data[0]);
        UnityEngine.Debug.Log(" data " + levelData.levels[0].level_data[1]);
        UnityEngine.Debug.Log(" data " + levelData.levels[0].level_data[2]);
        UnityEngine.Debug.Log(" data " + levelData.levels[0].level_data[3]);
    }
}
