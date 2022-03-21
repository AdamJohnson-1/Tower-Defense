using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{

    public static float relativeTowerPosX = 1.25f;
    public static float relativeTowerPosY = 1.9f;
    public static float relativeTowerPosZ = -1.25f;

    private List<GameObject> towers = new List<GameObject>();

    public bool addTower(GameObject tower, GameObject selectedNode)
    {

        Vector3 nodePosition = selectedNode.transform.position;
        float x = nodePosition.x;
        float y = nodePosition.y;
        float z = nodePosition.z;

        Vector3 towerPosition = new Vector3(
            x + TowerHandler.relativeTowerPosX,
            y + TowerHandler.relativeTowerPosY,
            z + TowerHandler.relativeTowerPosZ);

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
        return !nodeObj.GetComponent<Node>().hasTower;
    }
}
