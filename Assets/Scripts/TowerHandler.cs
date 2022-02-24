using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{

    private List<GameObject> towers = new List<GameObject>();

    public bool addTower(GameObject tower, GameObject selectedNode)
    {

        Vector3 nodePosition = selectedNode.transform.position;
        float x = nodePosition.x;
        float y = nodePosition.y;
        float z = nodePosition.z;

        Vector3 towerPosition = new Vector3(
            x + 1,
            y + 1,
            z - 1);

        GameObject newTower = Instantiate(tower, towerPosition, Quaternion.identity);
        towers.Add(newTower);

        selectedNode.GetComponent<Node>().hasTower = true;

        return true;
    }

    public bool removeTower()
    {
        //to be implemented

        //remove gameobject
        //find corresponding node and set hasTower to false;

        return false;
    }

    public bool checkIfValidTowerLocation(GameObject nodeObj)
    {
        /*Vector3 nodePosition = nodeObj.transform.position;

        for (int i = 0; i < towers.Count; i++ )
        {
            Vector3 towerPosition = towers[i].transform.position;
            if (towerPosition.x == nodePosition.x + 1 &&
                towerPosition.y == nodePosition.y + 1 &&
                towerPosition.z == nodePosition.z - 1)
                return false;
        }

        return true;*/

        return !nodeObj.GetComponent<Node>().hasTower;
    }
}
