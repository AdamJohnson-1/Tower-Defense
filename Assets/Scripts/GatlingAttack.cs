using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingAttack : AttackTower
{
    public float attackDamage = 5f;
    public float attackDelay = 5f;
    private Animator anim;
    private ParticleSystem pSystem;
    private Light pLight;
    private GameObject target;


    new void Update()
    {
        base.Update();

        if (target != null)
        {
            Vector3 rotation = new Vector3();
            rotation = target.transform.position - gameObject.transform.position;
            rotation.y = 0;
            //rotation.z = 0;
            gameObject.transform.rotation = Quaternion.LookRotation(rotation);
        }
    }

    public override float AttackDelay()
    {
        return attackDelay;
    }

    public override float GetDefaultRange()
    {
        return 20.0f;
    }

    public override int GetTowerPrice()
    {
        return 120;
    }

    public void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        pSystem = gameObject.GetComponentInChildren<ParticleSystem>();
        pLight = gameObject.GetComponentInChildren<Light>();
        countUpToShoot = AttackDelay();
    }

    //This is called when the tower attacks or does it's special effect
    public override void Animate(List<GameObject> targetedEnemies)
    {
        if (targetedEnemies.Count == 0)
        {
            anim.enabled = false;
            pSystem.Stop();
            pLight.enabled = false;
        }
        else
        {
            anim.enabled = true;
            pSystem.Play();
            pLight.enabled = true;
        }
    }


    public override float GetDamage(GameObject enemy)
    {
        return attackDamage;
    }

    public override List<GameObject> FilterTargets(List<GameObject> enemies)
    {
        List<GameObject> myEnemies = new List<GameObject>();
        if (target != null && CalcDistance(target) < GetDefaultRange())
        {
            target = null;
        }
        if (target == null)
        {
            foreach (GameObject enemy in enemies)
            {
                if (CalcDistance(enemy) < GetDefaultRange())
                {
                    target = enemy;
                    break;
                }
            }
            if (target == null)
            {
                return myEnemies;
            }
        }
        
        myEnemies.Insert(0,target);
        return myEnemies;
    }

}
