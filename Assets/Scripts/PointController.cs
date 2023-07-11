using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PointController : MonoBehaviour
{
    public Image original;
    public Sprite spriteAfterClick;
    public enum pointStates
    {
        unclicked,
        clicked
    };

    GameController gameController;

    pointStates currentPointState = pointStates.unclicked;

    public Point pointInPointController;
    public void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void PointClicked()
    {   //If the point is the first point in the list of points
        if (pointInPointController.pointNumber == 1)
        {
            ChangeAfterCLick();
            gameController.pointsToDrawLinesTo.Add(gameController.PrieviousClickedButtonNumber);
        }//If the point is last point clicked is equal to the current point minus 1
        else if (gameController.PrieviousClickedButtonNumber == pointInPointController.pointNumber - 1)
        {
            ChangeAfterCLick();
            gameController.pointsToDrawLinesTo.Add(gameController.PrieviousClickedButtonNumber);

            if (gameController.points.ElementAt(gameController.points.Count - 1).pointNumber == pointInPointController.pointNumber)
            {
                gameController.pointsToDrawLinesTo.Add(gameController.PrieviousClickedButtonNumber);
                gameController.pointsToDrawLinesTo.Add(1);
            }
        }//If the point is the last point in the instantiated list of points
    }

    public void ChangeAfterCLick()
    {
        if (currentPointState == pointStates.unclicked)
        {
            currentPointState = pointStates.clicked;
            original.sprite = spriteAfterClick;
            UnityEngine.Debug.Log("The point nubmer when clicked " + pointInPointController.pointNumber.ToString());
            gameController.PrieviousClickedButtonNumber = pointInPointController.pointNumber;
        }
    }
}
