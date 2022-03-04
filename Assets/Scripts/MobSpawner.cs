using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject walker;
    private static int boardLength;
    private static int boardWidth;

    // Start is called before the first frame update
    void Start()
    {
        // todo: not hard-code values for board size
        GameObject board = GameObject.FindWithTag("Ground");
        
        var meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();
        // var size = meshFilters[0].mesh.bounds.size;
        
        // boardLength = (int) size.x;
        // boardWidth = (int) size.y;
        
        boardLength = 48;
        boardWidth = 48;

        InvokeRepeating("SpawnWalker", 5, 2);
    }

    void SpawnWalker() {
        int fromTop = Random.Range(0, 2);

        int x = boardWidth;
        int z = boardLength;

        if (fromTop == 1) {
            x = Random.Range(1, boardWidth);
        } else {
            z = Random.Range(1, boardLength);
        }

        Instantiate(walker, new Vector3(x, 0, z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
