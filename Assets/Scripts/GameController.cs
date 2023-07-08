using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    DataHandler dataHandlerScript;
    LevelData levelData;
    public List<Point> points = new List<Point>();
    public GameObject pointPrefab;
    public Camera camera;
    public RectTransform parentCanvas;
    public RectTransform button;

    void Start()
    {
        dataHandlerScript = GameObject.FindGameObjectWithTag("DataHandler").GetComponent<DataHandler>();//Getting a reference to the script
        levelData = dataHandlerScript.LoadData();//Retrieving all of the data
        points = AssignPointCoordinates(0);
        createPointsOnScreen(points);
    }

    void Update()
    {
        
    }

    //Create a list of points
    public List<Point> AssignPointCoordinates(int arrayNumber)//Array number says which row of data is used from the json retrieved array of data
    {
        int pointCounter = 0;// Counter to assign a number to each of the elements 
        for (int i = 0; i < levelData.levels[arrayNumber].level_data.Length - 1; i += 2)//Go through all the members of the array
        {
            pointCounter++;
            points.Add( new Point( int.Parse( levelData.levels[arrayNumber].level_data[i] ), int.Parse( levelData.levels[arrayNumber].level_data[i + 1] ), pointCounter ) );
        }
        //Checking the points are created correctly
        // foreach (var point in points)
        // {
        //     UnityEngine.Debug.Log("Point x " + point.x + " y: " + point.y + " the point number is : " + point.pointNumber);
        // }
        return points;
    }

    public void createPointsOnScreen(List<Point> points)
    {
        foreach (var point in points)
        {
            //Vector3 spawnPosition = new Vector3( (point.x / 10) - 5, (-(point.y) / 20) + 2.5f, 0.0f);// y to invert increase Y while going down
            //Vector3 spawnPosition = new Vector3( point.x / 2,  -(point.y) / 2, 0.0f);// y to invert increase Y while going down
            //Vector3 localScalePosition = GameObject.FindGameObjectWithTag("Canvas").transform.InverseTransformPoint(spawnPosition);
            //GameObject instantiatedObject = Instantiate(pointPrefab, Camera.main.ScreenToWorldPoint(spawnPosition), Quaternion.identity);
            
            //pointPrefab.GetComponentInChildren<Text>().text = point.pointNumber.ToString();
            //Instantiating the button object
            GameObject instantiatedObject = Instantiate(pointPrefab, button.anchoredPosition, Quaternion.identity);
            Vector2 screenPosition = new Vector2(point.x + 800, point.y );//world position
            //Conversion of the global position to local canvas as the parent position
            Vector2 anchorPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas, screenPosition, camera, out anchorPosition);
            button.anchoredPosition = anchorPosition;
            instantiatedObject.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        }
    }
}
