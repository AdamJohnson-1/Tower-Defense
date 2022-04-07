using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public static Shop shopInstance;

    public int startingMoney;
    private int currentMoney = 0;
    public Text moneyText;

    private float secondsSinceLastMoneyDump;
    public float secondsPerMoneyDump = 5f;
    public int moneyPerDump = 10;

    public ErrorMessageScript errorMessageScript;
    public ActionHandlerScript actionHandlerScript;

    public GameObject standardTower;
    public GameObject standardTowerHologram;

    void Start()
    {
        shopInstance = this;
        changeMoney(startingMoney);
    }

    void Update()
    {
        //updates money regularly
        secondsSinceLastMoneyDump += Time.deltaTime;
        if(secondsSinceLastMoneyDump >= secondsPerMoneyDump)
        {
            secondsSinceLastMoneyDump -= secondsPerMoneyDump;
            changeMoney(moneyPerDump);
        }
    }

    private void instanceClickPurchaseTurretButton(GameObject tower, GameObject towerHologram)
    {
        actionHandlerScript.selectTowerToPlace(tower, towerHologram);
    }

    private void instanceChangeMoney(int amount)
    {
        currentMoney += amount;
        moneyText.text = "$" + currentMoney;
    }

    private int instanceGetMoney()
    {
        return currentMoney;
    }

    private bool instanceCheckIfMoneyGreaterOrEqual(int amount)
    {
        return currentMoney >= amount;
    }



    //static methods for singleton pattern - not completely implemented yet
    public static void clickPurchaseTurretButton(GameObject tower, GameObject towerHologram)
    {
        if (Shop.shopInstance)
        {
            Shop.shopInstance.instanceClickPurchaseTurretButton(tower, towerHologram);
        }
    }

    public static int getMoney()
    {
        if(Shop.shopInstance)
        {
            return Shop.shopInstance.instanceGetMoney();
        } else
        {
            return 0;
        }
    }
    public static void changeMoney(int amount)
    {
        if (Shop.shopInstance)
        {
            Shop.shopInstance.instanceChangeMoney(amount);
        }
    }
    public static bool checkIfMoneyGreaterOrEqual(int amount)
    {
        if (Shop.shopInstance)
        {
            return Shop.shopInstance.instanceCheckIfMoneyGreaterOrEqual(amount);
        }
        else
        {
            return false;
        }
    }
}
