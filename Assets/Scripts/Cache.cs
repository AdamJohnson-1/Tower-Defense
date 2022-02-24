using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cache will be used to store information that needs to be used by multiple objects and
//will be implemented as a singleton

public class Cache : MonoBehaviour
{

    public float totalLife = 100f;
    public int startingGold = 100;

    private float currentLife;
    private int currentGold;
    private Cache cache;
    private static int score;

    private void Awake()
    {
        if (cache == null)
        {
            cache = this;
        }
        else
        {
            Debug.Log("Second instance of Cache");
        }
    }
    public static void setScore(int newScore)
    {
        score = newScore;
    }

    public static int getScore()
    {
        return score;
    }

    public Cache getCache()
    {
        return cache;
    }

    public void setCurrentGold(int gold)
    {
        currentGold = gold;
    }
    public int getCurrentGold()
    {
        return currentGold;
    }

    public void setCurrentLife(float life)
    {
        currentLife = life;
    }
    public float getCurrentLife()
    {
        return currentLife;
    }

}
