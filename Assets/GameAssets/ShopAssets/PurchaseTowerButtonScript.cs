using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseTowerButtonScript : MonoBehaviour
{

    public GameObject tower;
    public GameObject towerHologram;

    public Text NameText;
    public Text CostText;

    void Start()
    {

        AttackTower towerScript = tower.GetComponent<AttackTower>();
        NameText.text = towerScript.GetTowerName();
        CostText.text = "$" + towerScript.GetTowerPrice().ToString();

        GetComponent<Button>().onClick.AddListener(() =>
        {
            Shop.clickPurchaseTurretButton(tower, towerHologram);
        });
    }

}
