using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackTower : MonoBehaviour
{
    public float thisTime = 0;

    public abstract float GetDamage(GameObject enemy);

    //Returns a float, example 2.5 seconds
    public abstract float AttackDelay();

    //This is called when the tower attacks or does it's special effect
    public virtual void Animate(List<GameObject> targetedEnemies)
    {
        //Does nothing by default
        //When a tower has an animation this method should be overriden
    }

    //returns only the enemies upon which this particular tower will have an effect
    public abstract List<GameObject> FilterTargets(GameObject[] enemies);


    public void Update()
    {
        thisTime += Time.deltaTime;
        if (thisTime > AttackDelay())
        {
            var enemies = GameObject.FindGameObjectsWithTag("horde");
            var filteredEnemies = FilterTargets(enemies);
            Animate(filteredEnemies);
            foreach (GameObject enemy in filteredEnemies)
            {
                    Debug.Log("Shooting enemy");
                    float damage = GetDamage(enemy);
                    enemy.GetComponent<Horde>().Damage(damage, gameObject);
                }
            }
            thisTime = 0;
        }
    }
