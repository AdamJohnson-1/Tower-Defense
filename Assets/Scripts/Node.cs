using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class Node : MonoBehaviour
{
    private Renderer myRenderer;
    private Color startColor;

    public GameObject tower = null;

    private MouseInputHandlerScript mouseInputHandler;

    // Start is called before the first frame update
    void Start()
    {
        mouseInputHandler = GameObject.FindWithTag("Global").GetComponent< MouseInputHandlerScript>();

        myRenderer = gameObject.GetComponent<Renderer>();
        startColor = myRenderer.material.color;
    }
    private void OnMouseEnter()
    {
        //so the node isn't highlighted through the User Interface
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        mouseInputHandler.onMouseEnterNode(gameObject);
    }
    private void OnMouseExit()
    {
        mouseInputHandler.onMouseLeaveNode(gameObject);
    }
}
