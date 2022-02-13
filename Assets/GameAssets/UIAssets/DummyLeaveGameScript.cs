using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DummyLeaveGameScript : MonoBehaviour
{
    public string endGameScene;

    public void leaveToEndGameScene()
    {
        SceneManager.LoadScene(endGameScene);
    }
}
