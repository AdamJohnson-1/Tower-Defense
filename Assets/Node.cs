using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Renderer myRenderer;
    private Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
        startColor = myRenderer.material.color;
    }
    private void OnMouseEnter()
    {
        myRenderer.material.color = Color.gray;
    }
    private void OnMouseExit()
    {
        myRenderer.material.color = startColor;
    }

    public GameObject tower;

    private void OnMouseDown()
    {
        Vector3 nodePosition = gameObject.transform.position;
        float x = nodePosition.x;
        float y = nodePosition.y;
        float z = nodePosition.z;

        Vector3 towerPosition = new Vector3(
            gameObject.transform.eulerAngles.x + gameObject.transform.localScale.x / 2,
            gameObject.transform.eulerAngles.y + gameObject.transform.localScale.y/2,
            gameObject.transform.eulerAngles.z + +gameObject.transform.localScale.z / 2);
        
        Instantiate(tower, towerPosition, Quaternion.identity);
    }
}
