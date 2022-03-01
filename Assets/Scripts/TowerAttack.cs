using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerAttack : MonoBehaviour
{
    public float shootDelay = 0.35f;
    private float countUpToShoot = 0;

    public abstract float getTowerRange();
    public abstract int getCost();

    // Update is called once per frame

    protected void Init()
    {
        countUpToShoot = shootDelay; // allow tower to shoot right after placement
    }

    void Update()
    {
        countUpToShoot += Time.deltaTime;

        if (countUpToShoot > shootDelay)
        {
            GameObject[] enemies = GetHorde();
            if (enemies != null)
            {
                enemies = FilterTargets(enemies);
                foreach (GameObject enemy in enemies)
                {
                    float damage = GetDamage(enemy);
                    enemy.GetComponent<Horde>().Damage(damage, gameObject);

                }
                countUpToShoot = 0;
                AnimateFiring();
            }
          
        }
        else
        {
            AnimateNotFiring();
        }
    }
    protected abstract void AnimateFiring();
    protected abstract void AnimateNotFiring();
    protected abstract float GetDamage(GameObject enemy);
    protected abstract GameObject[] FilterTargets(GameObject[] enemies);

    private GameObject[] GetHorde()
    {
        return GameObject.FindGameObjectsWithTag("horde");
    }

    protected float CalcDistance(GameObject enemy)
    {
        Vector3 towerPosition = gameObject.transform.position;
        Vector3 enemyPosition = enemy.gameObject.transform.position;

        return Vector3.Distance(towerPosition, enemyPosition);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, getTowerRange());
    }
}
