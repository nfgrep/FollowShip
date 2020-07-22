using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{

    public GameObject scoreNumber;
    public GameObject timeNumber;
    static public int timeReset;
    static public int currentTime;
    Text score;
    Text timeTxt;
    static public int currentScore = 0;
    Scene currentScene;

    void Start()
    {
        //Stores the current scene
        currentScene = SceneManager.GetActiveScene();
        //Stores the respective text components
        timeTxt = timeNumber.GetComponent<Text>();
        score = scoreNumber.GetComponent<Text>();
        //Sets the text at start
        timeTxt.text = "" + 0;
        score.text = "" + currentScore;
    }

    void Update()
    {
        //Checks what the current scene is and either resets if returned to title
        if (currentScene.name == "Lvl1")
        {
            currentTime = Mathf.RoundToInt(Time.time) - timeReset;
        }

        else if (currentScene.name == "Title")
        {
            currentScore = 0;
            ResetTime();
        }
        score.text = "" + currentScore;
        timeTxt.text = "" + currentTime;
    }

    //A function to be called upon by player via SendMessage
    void AddScore()
    {
        currentScore++;
    }

    //Resets the time element
    void ResetTime()
    {
        timeReset = Mathf.RoundToInt(Time.time);
    }

}
