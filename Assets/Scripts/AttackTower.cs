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
    public abstract List<GameObject> FilterTargets(List<GameObject> enemies);


    public void Update()
    {
        thisTime += Time.deltaTime;
        if (thisTime > AttackDelay())
        {
            var enemies = GameObject.FindGameObjectsWithTag("horde");
            var filteredEnemies = FilterTargets(new List<GameObject> (enemies));
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

    public float CalcDistance(GameObject enemy)
    {
        Vector3 towerPosition = gameObject.transform.position;
        Vector3 enemyPosition = enemy.gameObject.transform.position;

        return Vector3.Distance(towerPosition, enemyPosition);
    }


    //Used solely for the interface when placing a tower. 
    public abstract float GetDefaultRange();


    //The price an implementation of the tower can be purchased for
    public abstract int GetTowerPrice();


}
