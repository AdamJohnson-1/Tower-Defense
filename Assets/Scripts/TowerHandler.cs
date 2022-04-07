using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{

    public static TowerHandler TowerHandlerInstance;

    public static float relativeTowerPosX = 1.25f;
    public static float relativeTowerPosY = 1.9f;
    public static float relativeTowerPosZ = -1.25f;

    private List<GameObject> towers = new List<GameObject>();

    void Start()
    {
        TowerHandler.TowerHandlerInstance = this;
    }

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

        selectedNode.GetComponent<Node>().tower = newTower;

        return true;
    }

    public bool removeTowerFromNode(GameObject nodeObj)
    {
        if (nodeObj == null)
            return false;

        Node nodeScript = nodeObj.GetComponent<Node>();
        GameObject tower = nodeScript.tower;

        //NEEDS TO BE IMPLEMENTED!!!

        nodeScript.tower = null;
        Destroy(tower);

        return true;
    }

    public bool checkIfValidTowerLocation(GameObject nodeObj)
    {
        if (nodeObj == null)
            return false;
        return (nodeObj.GetComponent<Node>().tower == null);
    }
}
