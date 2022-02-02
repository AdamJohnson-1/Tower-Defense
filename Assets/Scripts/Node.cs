using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class Node : MonoBehaviour
{
    private Renderer myRenderer;
    private Color startColor;
    public GameObject tower;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
        startColor = myRenderer.material.color;
    }
    private void OnMouseEnter()
    {
        //so the node isn't highlighted through the User Interface
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        myRenderer.material.color = Color.gray;
    }
    private void OnMouseExit()
    {
        myRenderer.material.color = startColor;
    }

    private void OnMouseDown()
    {
        //so the player doesn't accidentially place a turret through the User Interface
        if (EventSystem.current.IsPointerOverGameObject())
            return;
            
        if (tower == null)
        {
            Debug.Log("Tower not assigned");
        }


        Vector3 nodePosition = gameObject.transform.position;
        float x = nodePosition.x;
        float y = nodePosition.y;
        float z = nodePosition.z;

        Vector3 towerPosition = new Vector3(
            x + 1,
            y + 2,
            z - 1);

        Instantiate(tower, towerPosition, Quaternion.identity);
    }
}
