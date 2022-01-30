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

        
        Instantiate(tower);
    }
}
