using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class MouseInputHandlerScript : MonoBehaviour
{

    private Cache cacheScript;
    private ActionHandlerScript actionScript;

    void Start()
    {
        cacheScript = gameObject.GetComponent<Cache>();
        actionScript = gameObject.GetComponent<ActionHandlerScript>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            actionScript.onLeftMouseDown();

        if (Input.GetMouseButtonDown(1))
            actionScript.onRightMouseDown();
    }

    public void onMouseEnterNode(GameObject nodeObject)
    {
        //so the node isn't highlighted through the User Interface
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        actionScript.onMouseEnterNode(nodeObject);
    }

    public void onMouseLeaveNode(GameObject nodeObject)
    {
        actionScript.onMouseLeaveNode(nodeObject);
    }
}
