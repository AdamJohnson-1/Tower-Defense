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

    public override List<GameObject> FilterTargets(List<GameObject> enemies)
    {
        GameObject closest = null;
        var dist = float.PositiveInfinity;
        foreach (GameObject e in enemies)
        {
            if (CalcDistance(e) < dist)
            {
                closest = e;
            }
        }
        

        if (closest == null) { return new List<GameObject> { }; }
        return new List<GameObject> { closest };
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
        return 120;
    }
}
