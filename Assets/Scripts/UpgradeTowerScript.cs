using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTowerScript : MonoBehaviour
{

    public static UpgradeTowerScript UpgradeTowerScriptInstance = null;

    public Text NameText;
    public Text LevelText;
    public Text UpgradeCostText;

    public GameObject Container;

    private GameObject CurrentNode = null;

    void Start()
    {
        UpgradeTowerScript.UpgradeTowerScriptInstance = this;

        //Must be initially active to set the instance.
        Container.SetActive(false);
    }


    public void SelectTower(GameObject nodeObj)
    {
        CurrentNode = nodeObj;
        GameObject tower = CurrentNode.GetComponent<Node>().tower;
        AttackTower towerScript = tower.GetComponent<AttackTower>();

        NameText.text = towerScript.GetTowerName();
        LevelText.text = towerScript.GetTowerlevel().ToString();
        UpgradeCostText.text = "Upgrade  -  $" + towerScript.GetTowerUpgradePrice().ToString();

        Container.SetActive(true);
    }

    public void DeselectTower()
    {
        CurrentNode = null;
        Container.SetActive(false);

        ActionHandlerScript.ActionHandlerScriptInstance.deselectTowerToEdit();
    }

    public void DestroyTower()
    {
        TowerHandler.TowerHandlerInstance.removeTowerFromNode(CurrentNode);
        DeselectTower();
    }

    public void UpgradeTower()
    {
        GameObject tower = CurrentNode.GetComponent<Node>().tower;
        AttackTower towerScript = tower.GetComponent<AttackTower>();

        int cost = towerScript.GetTowerUpgradePrice();

        if (Shop.checkIfMoneyGreaterOrEqual(cost))
        {
            towerScript.UpgradeTower();
            SelectTower(CurrentNode); // update text boxes
        } else
        {
            ErrorMessageScript.ErrorMessageScriptInstance.ShowMessage("Not enough money.");
        }
    }
}
