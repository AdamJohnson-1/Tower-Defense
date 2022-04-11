using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Http;

public class EndGameScript : MonoBehaviour
{
    public string mainMenuScene;
    private int curScoreincrementer;
    private int totalScore;

    public Text scoreText;
    public Text worldRecordText;

    void Start()
    {
        //fetch player score
        curScoreincrementer = 0;
        totalScore = Cache.getScore();
        Cache.resetScore();

        GetWorldRecord();
    }



    async void GetWorldRecord()
    {
        using var client = new HttpClient();
        var content = await client.GetStringAsync("https://students.cs.byu.edu/~jscholl4/towerdefense/score.php");
        worldRecordText.text = content;
        Debug.Log(content);

        if (totalScore > int.Parse(worldRecordText.text)) 
        {
            await client.PostAsync("https://students.cs.byu.edu/~jscholl4/towerdefense/update.php", new StringContent(totalScore.ToString()));
        }
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
