using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingVanguard : MonoBehaviour
{
    public GameObject guardType;
    // Start is called before the first frame update
    void Awake()
    {
        InvokeRepeating("makeGuard", 0f, 4f);
        InvokeRepeating("makeGuard", .1f, 4f);
    }
    private void makeGuard()
    {
        Instantiate(guardType, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
      
      
}
