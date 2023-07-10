using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    {
        if (pointInPointController.pointNumber == 1)
        {
            ChangeAfterCLick();
            gameController.pointsToDrawLinesTo.Add(gameController.PrieviousClickedButtonNumber);
        }
        else if (gameController.PrieviousClickedButtonNumber == pointInPointController.pointNumber - 1)
        {
            ChangeAfterCLick();
            gameController.pointsToDrawLinesTo.Add(gameController.PrieviousClickedButtonNumber);
        }        
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


    public void drawLine()
    {

    }

    // public bool lineFinished()
    // {

    // }
}
