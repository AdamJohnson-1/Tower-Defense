using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    public string mainMenuScene;
    private int curScoreincrementer;
    private int totalScore;

    public Text scoreText;

    void Start()
    {
        //fetch player score
        curScoreincrementer = 0;
        totalScore = Cache.getScore();
        Cache.resetScore();
    }

    void Update()
    {

        if (curScoreincrementer < totalScore)
        {
            curScoreincrementer += (int)Math.Ceiling(Time.deltaTime * totalScore / 3);
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
