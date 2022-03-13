using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStrategy {
    protected List<GameObject> enemies;
    public AttackStrategy(List<GameObject> enemies)
    {
        this.enemies = enemies;
    }
    public abstract List<GameObject> selectTargets(AttackTower script);

}

public abstract class AttackTemplateStrategy : AttackStrategy
{
    public AttackTemplateStrategy(List<GameObject> enemies) : base(enemies)
    { }

    public override List<GameObject> selectTargets(AttackTower script)
    {
        GameObject currentTarget = null;
        List<GameObject> myEnemies = new List<GameObject>();
        foreach (GameObject newEnemy in enemies)
        {
            if (script.CalcDistance(newEnemy) < script.GetDefaultRange())
            {
                if (currentTarget != null)
                {
                    currentTarget = TargetComparison(script, currentTarget, newEnemy);
                }
                else
                {
                    currentTarget = newEnemy;
                }
                
            }
        }
        if (currentTarget == null)
        {
            return myEnemies;
        }
        myEnemies.Insert(0, currentTarget);
        return myEnemies;
    }

    public abstract GameObject TargetComparison(AttackTower script,
    GameObject currentTarget, GameObject newTarget);

}


public class AttackDefaultStrategy : AttackTemplateStrategy
{
    public AttackDefaultStrategy(List<GameObject> enemies) : base(enemies)
    { }

    public override GameObject TargetComparison(AttackTower script, GameObject currentTarget, GameObject newTarget)
    {
        return currentTarget;
    }
}

public class AttackFastestStrategy : AttackTemplateStrategy
{
    public AttackFastestStrategy(List<GameObject> enemies) : base(enemies)
    { }

    public override GameObject TargetComparison(AttackTower script, GameObject currentTarget, GameObject newTarget)
    {
        if (currentTarget.GetComponent<MobScript>().DefaultMoveSpeed >= newTarget.GetComponent<MobScript>().DefaultMoveSpeed)
        {
            return currentTarget;
        }
        else
        {
            return newTarget;
        }
    }
}
