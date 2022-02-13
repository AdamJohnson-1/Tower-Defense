using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    public string mainMenuScene;
    private int curScoreincrementer = 0;
    private int totalScore;

    public Text scoreText;

    void Start()
    {
        //fetch player score
        totalScore = 891;
    }

    void Update()
    {

        if (curScoreincrementer < totalScore)
        {
            curScoreincrementer += 1;
            if (curScoreincrementer > totalScore)
            {
                curScoreincrementer = totalScore;
            }
            scoreText.text = curScoreincrementer.ToString();

        }
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
