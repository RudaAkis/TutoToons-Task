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
        TestPrint();
    }

    [SerializeField] 
    string filePath = "Assets/Scripts/level_data.json";// The path to the json file

    private LevelData levelData;// Reference to the object that stores all the arrays of the JSON file


    private void LoadData()
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            string jsonRaw = reader.ReadToEnd();//Reads the whole JSON file and returns the raw data
            levelData = JsonUtility.FromJson<LevelData>(jsonRaw);//Formats the JSON files' raw data into the appropriate classes
        }
    } 

    public void TestPrint()
    {
        UnityEngine.Debug.Log(" The length of the data " + levelData.levels[0].level_data.Length);// To read all of the data in a single line of the json file
        UnityEngine.Debug.Log(" The length of the data " + levelData.levels[1].level_data.Length);// To read all of the data in a single line of the json file
        UnityEngine.Debug.Log(" The length of the data " + levelData.levels[2].level_data.Length);// To read all of the data in a single line of the json file
        UnityEngine.Debug.Log(" The length of the data " + levelData.levels[3].level_data.Length);// To read all of the data in a single line of the json file
        UnityEngine.Debug.Log(" data " + levelData.levels[0].level_data[1]);//Prints out the single data point at the second poistion of the first array in the file
    }

    
}
