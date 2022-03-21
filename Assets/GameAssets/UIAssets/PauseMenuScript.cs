using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{

    public string endGameScene;
    public GameObject PauseButton;
    public GameObject PauseMenu;

    public static string StaticEndGameScene;
    public static GameObject StaticPauseButton;
    public static GameObject StaticPauseMenu;


    /*private static PauseMenuScript PauseMenuScriptInstance;*/

    void Start()
    {
        //PauseMenuScript.PauseMenuScriptInstance = this;
        PauseMenuScript.StaticEndGameScene = endGameScene;
        PauseMenuScript.StaticPauseButton = PauseButton;
        PauseMenuScript.StaticPauseMenu = PauseMenu;
    }

    public static void PauseGame()
    {

        Time.timeScale = 0f;
        StaticPauseMenu.SetActive(true);
        StaticPauseButton.SetActive(false);
    }

    public static void EndGame()
    {
        Time.timeScale = 1.0f;
        StaticPauseMenu.SetActive(false);
        StaticPauseButton.SetActive(true);
        SceneManager.LoadScene(StaticEndGameScene);
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1.0f;
        StaticPauseMenu.SetActive(false);
        StaticPauseButton.SetActive(true);
    }

}
