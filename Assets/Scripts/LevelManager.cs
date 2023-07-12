using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int levelNumber;
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void assignLevel()
    {
        gameController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GameController>();
        gameController.level = levelNumber;
    }
}
