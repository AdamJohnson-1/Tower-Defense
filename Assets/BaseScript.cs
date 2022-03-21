using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBaseScript : MonoBehaviour {


    private static GameBaseScript baseScriptInstance;

    public int startingLives;
    private int currentLives;

    public string endGameScene;
    public Text livesText;

    void Start()
    {
        currentLives = startingLives;
        GameBaseScript.baseScriptInstance = this;
        instanceSetLives(currentLives);
    }

    public static void setLives(int amount)
    {
        if(GameBaseScript.baseScriptInstance)
        {
            GameBaseScript.baseScriptInstance.instanceSetLives(amount);
        }
    }

    public void instanceSetLives(int amount)
    {
        currentLives = amount;

        if(amount <= 0)
        {
            SceneManager.LoadScene(endGameScene);

        } else if (livesText)
        {
            livesText.text = currentLives.ToString();
        }
    }

    public static void subtractLives(int amount)
    {
        if (GameBaseScript.baseScriptInstance)
        {
            GameBaseScript.baseScriptInstance.instanceSubtractLives(amount);
        }
    }

    public void instanceSubtractLives(int amount)
    {
        instanceSetLives(currentLives - amount);
    }
}
