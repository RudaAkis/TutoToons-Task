using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    DataHandler dataHandlerScript;
    LevelData levelData;

    public List<Point> points = new List<Point>();

    public GameObject pointPrefab;
    public Camera camera;
    public RectTransform parentCanvas;
    public RectTransform button;

    public TextMeshProUGUI pointNumberText;
    public RectTransform textRect;

    void Start()
    {
        dataHandlerScript = GameObject.FindGameObjectWithTag("DataHandler").GetComponent<DataHandler>();//Getting a reference to the script
        levelData = dataHandlerScript.LoadData();//Retrieving all of the data
        points = AssignPointCoordinates(2);
        createPointsOnScreen(points);
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
        return points;
    }

    public void createPointsOnScreen(List<Point> points)
    {
        foreach (var point in points)
        {
            TextMeshProUGUI instantiatedText = Instantiate(pointNumberText, textRect.anchoredPosition, Quaternion.identity);
            Vector2 textScreenPosition = new Vector2(point.x + 100, (point.y / 2) + 150);//world position
            Vector2 anchorPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas, textScreenPosition, camera, out anchorPosition);
            textRect.anchoredPosition = anchorPosition;
            instantiatedText.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

            //Instantiating the button object
            GameObject instantiatedObject = Instantiate(pointPrefab, button.anchoredPosition, Quaternion.identity);
            Vector2 screenPosition = new Vector2(point.x + 100 ,(point.y / 2) + 150);//world position
            //Conversion of the global position to local canvas as the parent position
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas, screenPosition, camera, out anchorPosition);
            button.anchoredPosition = anchorPosition;
            instantiatedObject.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            
        }
    }
}

            // buttonText = instantiatedObject.GetComponent<TextMeshProUGUI>();
            // buttonText.SetText(textForButton);
            // buttonText.SetText("Fuckign Text");
            // UnityEngine.Debug.Log( " 66666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666" + buttonText.text);
            // // if (buttonText != null)
            // // {
            // //     buttonText.text = Convert.ToString(point.pointNumber);
            // // }