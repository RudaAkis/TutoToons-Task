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

    private Point point;

    pointStates currentPointState = pointStates.unclicked;

    public void PointClicked()
    {
        if (currentPointState == pointStates.unclicked)
        {
            currentPointState = pointStates.clicked;
            original.sprite = spriteAfterClick;

            //TO DO 
            /*
                Create the fade in fade out animation for the swithing of the sprite
            */
        }
    }
}
