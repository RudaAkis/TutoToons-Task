using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class GameController : MonoBehaviour
{
  DataHandler dataHandlerScript;
  LevelData levelData;

  public List<Point> points = new List<Point>();//List of point coordinates with their point number
  public List<GameObject> instantiatedPoints = new List<GameObject>();//List of game objects(buttons) that are instantiated at their point coordinates
  public List<TextMeshProUGUI> instantiatedNumbers = new List<TextMeshProUGUI>();//List of text objects used to display the point number and fading it out after the line is drawn
  public List<int> pointsToDrawLinesTo = new List<int>();//Dynamic list that will receive all the points that have been clicked to store until a line is drawn between them

  public GameObject pointPrefab;// Object that will be instantiated to be seen and cliked on screen
  public Camera camera;
  public RectTransform parentCanvas;//Used to correctly place the point on screen
  public RectTransform button;//Used to correctly place the point on screen

  public TextMeshProUGUI pointNumberText;
  public RectTransform textRect;//Used to correctly place the point on screen

  PointController pointController;//Script compoent variable
  [HideInInspector]
  public int PrieviousClickedButtonNumber = 0;

  public LineRenderer lineRenderer;
  public float lineAnimationDuration;
  bool isDrawing = false;// Used to determine when one line is drawn
  int numberToDestroyCounter = 0;
  public int level;// The level that is assigned when a button is pressed in the menu Panel

  Animator textAnimator;
  bool hasRun = false;


  void Start()
  {
    level = -1;
    dataHandlerScript = GameObject.FindGameObjectWithTag("DataHandler").GetComponent<DataHandler>();//Getting a reference to the script
    levelData = dataHandlerScript.LoadData();//Retrieving all of the data
  }
  void Update()
  { //If the level is not negative and the methods has not run yet instantiate the points in their lecoations
    if (level != -1 && hasRun == false)
    {
      points = AssignPointCoordinates(level);
      instantiatedPoints = createPointsOnScreen(points);
      hasRun = true;
    }
    if (isDrawing == false && pointsToDrawLinesTo.Count > 1)
    {
      isDrawing = true;
      StartCoroutine(drawLine(instantiatedPoints, pointsToDrawLinesTo));
      pointsToDrawLinesTo.RemoveAt(0);

      FadeOutDeleteNumber(numberToDestroyCounter, instantiatedNumbers);
      numberToDestroyCounter++;
    }
  }

  //Create a list of points
  public List<Point> AssignPointCoordinates(int arrayNumber)//Array number says which row of data is used from the json retrieved array of data
  {
    int pointCounter = 0;// Counter to assign a number to each of the elements 
    for (int i = 0; i < levelData.levels[arrayNumber].level_data.Length - 1; i += 2)//Go through all the members of the array
    {
      pointCounter++;
      points.Add(new Point(int.Parse(levelData.levels[arrayNumber].level_data[i]), int.Parse(levelData.levels[arrayNumber].level_data[i + 1]), pointCounter));
    }
    return points;
  }

  public List<GameObject> createPointsOnScreen(List<Point> points)
  {
    foreach (var point in points)
    {
      //Create the text for the button    
      TextMeshProUGUI instantiatedText = Instantiate(pointNumberText, textRect.anchoredPosition, Quaternion.identity);
      Vector2 textScreenPosition = new Vector2((((point.x * 100) / 1000) * Screen.width) / 100, (((point.y * 100) / 1000) * Screen.height) / 100 - 5);//world position
      Vector2 anchorPosition;
      RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas, textScreenPosition, camera, out anchorPosition);
      textRect.anchoredPosition = anchorPosition;
      instantiatedText.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
      instantiatedText.SetText(point.pointNumber.ToString());
      instantiatedNumbers.Add(instantiatedText);

      //Instantiating the button object
      GameObject instantiatedObject = Instantiate(pointPrefab, button.anchoredPosition, Quaternion.identity);
      Vector2 screenPosition = new Vector2((((point.x * 100) / 1000) * Screen.width) / 100, (((point.y * 100) / 1000) * Screen.height) / 100);//world position
                                                                                                                                              //Conversion of the global position to local canvas as the parent position
      RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas, screenPosition, camera, out anchorPosition);
      button.anchoredPosition = anchorPosition;
      instantiatedObject.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
      //Reassign the new coordinates according to the width and height of the screen
      point.x = (int)screenPosition.x;
      point.y = (int)screenPosition.y;
      //Assigne the Point data from the list to each individual point created
      pointController = instantiatedObject.GetComponent<PointController>();
      pointController.pointInPointController = point;

      instantiatedPoints.Add(instantiatedObject);
    }
    return instantiatedPoints;
  }

  public IEnumerator drawLine(List<GameObject> instantiatedPoints, List<int> pointsToDrawLinesTo)
  {
    lineRenderer = Instantiate(lineRenderer, instantiatedPoints.ElementAt(0).transform.position, Quaternion.identity);//Create a new line
    float startTime = Time.time;// Set the current start time
    lineRenderer.SetPosition(0, instantiatedPoints.ElementAt(pointsToDrawLinesTo.ElementAt(0) - 1).transform.position);// Set the instantiated lines position from which the line will be drawn
                                                                                                                       //Set the start and end vectors
    Vector3 startPosition = instantiatedPoints.ElementAt(pointsToDrawLinesTo.ElementAt(0) - 1).transform.position;
    Vector3 endPosition = instantiatedPoints.ElementAt(pointsToDrawLinesTo.ElementAt(1) - 1).transform.position;
    Vector3 pos = startPosition;//Vector that will extend during animation
    //Lerp the line renderer until it reaches the end point in the given animation time
    while (pos != endPosition)
    {
      float t = (Time.time - startTime) / lineAnimationDuration;
      pos = Vector3.Lerp(startPosition, endPosition, t);
      lineRenderer.SetPosition(1, pos);
      yield return null;
    }
    isDrawing = false;//Set is drawing to false to allow the next line to be drawn
  }
  //Animate the number to fade out of visibility (change the alpha value from 255 to 0)
  public void FadeOutDeleteNumber(int numberToDestroyCounter, List<TextMeshProUGUI> instantiatedNumbers)
  {
    //Retrieve the animator component of the text field at the counter
    textAnimator = instantiatedNumbers.ElementAt(numberToDestroyCounter).GetComponent<Animator>();
    textAnimator.SetBool("canFade", true);
    destroyAfterDelay(1.5f, instantiatedNumbers.ElementAt(numberToDestroyCounter));
  }
  //Destroy the text object after a delay
  public IEnumerator destroyAfterDelay(float delay, TextMeshProUGUI textToDestroy)
  {
    yield return new WaitForSeconds(delay);
    Destroy(textToDestroy);
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