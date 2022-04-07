using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMessageScript : MonoBehaviour {

    public static ErrorMessageScript ErrorMessageScriptInstance = null;

    private CanvasGroup canvGroup;
    public Text textbox;

    private float opacity = 0f;

    void Start()
    {
        ErrorMessageScript.ErrorMessageScriptInstance = this;
        canvGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {

        if(opacity > 0)
        {
            opacity -= Time.deltaTime;

            if(opacity <= 0)
            {
                opacity = 0;
            }

            canvGroup.alpha = opacity;
        }
    }

    public void ShowMessage(string msg)
    {
        opacity = 2f;
        textbox.text = msg;
    }
}
