using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : AttackTower
{
    private Animator anim;
    public LineRenderer lineRenderer;

    

    [Header("Laser Settings")]
    public float attackDamage = 50f;
    public float attackDelay = 5f;
    public Transform firePoint;
    public float laserVisibilityTime;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        countUpToShoot = AttackDelay(); // allow tower to shoot right after placement
    }
    public override float AttackDelay()
    {
        return 3f;
    }

    GameObject biggest = null;
    public override List<GameObject> FilterTargets(List<GameObject> enemies)
    {
        if (enemies.Contains(biggest)) { return new List<GameObject> { biggest }; }

        var health = 0f;
        foreach (GameObject e in enemies)
        {
            var mob = e.GetComponent<MobScript>();
            var h = mob.Health;
            if (h > health)
            {
                health = h;
                biggest = e;
            }
        }
        

        if (biggest == null) { return new List<GameObject> { }; }
        return new List<GameObject> { biggest };
    }

    public override float GetDamage(GameObject enemy)
    {
        return attackDamage + attackDamage * (TowerLevel - 1f) / 2;
    }

    public override float GetDefaultRange()
    {
        return 0;
    }

    public override int GetTowerPrice()
    {
        return 100;
    }


    public override string GetTowerName()
    {
        return "Gauss Cannon";
    }
    public override int GetTowerUpgradePrice()
    {
        return GetTowerPrice() + (TowerLevel * 70);
    }


    new void Update()
    {
        base.Update();

        if (biggest == null)
        {
            lineRenderer.enabled = false;
            var enemies = GameObject.FindGameObjectsWithTag("horde");
            FilterTargets(new List<GameObject>(enemies));
        }
        if (biggest != null)
        {
            Debug.Log("Turning to look at enemy");
            Vector3 lookVector = biggest.transform.position - transform.position;
            var rotation = Quaternion.LookRotation(lookVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);

            lineRenderer.SetPosition(0, firePoint.position);
            var dest = biggest.transform.position;
            dest.y += 0.2f;
            lineRenderer.SetPosition(1, dest);
        }

        if (countUpToShoot > laserVisibilityTime)
        {
            lineRenderer.enabled = false;
        }
    }

    public override void Animate(List<GameObject> targetedEnemies)
    {
        if (targetedEnemies.Count == 0)
        {
            anim.enabled = false;
            lineRenderer.enabled = false;
        }
        else
        {
            var laserDest = targetedEnemies[0].transform.position;
            lineRenderer.enabled = true;
            anim.enabled = true;
            anim.Play("");
        }
    }
}
