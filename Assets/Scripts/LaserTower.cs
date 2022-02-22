using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : AttackTower
{
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
        return 60f;
    }

    public override float GetDefaultRange()
    {
        return 0;
    }

    public override int GetTowerPrice()
    {
        return 80;
    }
}
