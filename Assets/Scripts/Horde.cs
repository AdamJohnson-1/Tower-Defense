using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horde : MonoBehaviour
{

    private float health = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Damage(float damage)
    {
        health -= damage;
        Debug.Log($"Health {health}");
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}
