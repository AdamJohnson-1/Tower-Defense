using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseTowerButtonScript : MonoBehaviour
{

    public GameObject tower;
    public GameObject towerHologram;

    public string towerName;
    public Text nameText;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Shop.clickPurchaseTurretButton(tower, towerHologram);
        });
    }

}
