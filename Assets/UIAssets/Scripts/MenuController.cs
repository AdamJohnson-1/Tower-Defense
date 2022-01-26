using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Levels to load")]

    public string createGameScene;

    public void CreateGame()
    {
        SceneManager.LoadScene(createGameScene);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
