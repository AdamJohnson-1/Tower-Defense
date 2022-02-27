using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingAttack : TowerAttack
{
    public float attackDamage = 5f;
    private Animator anim;
    private ParticleSystem pSystem;
    private Light pLight;
    private GameObject target;


    public override float getTowerRange() {return 20.0f;}
    public override int getCost() { return 120; }

    public void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        pSystem = gameObject.GetComponentInChildren<ParticleSystem>();
        pLight = gameObject.GetComponentInChildren<Light>();

        Init();
    }

    protected override void AnimateFiring()
    {
        anim.enabled = true;
        pSystem.Play();
        pLight.enabled = true;

    }

    protected override void AnimateNotFiring()
    {
        anim.enabled = false;
        pSystem.Stop();
        pLight.enabled = false;
    }

    protected override GameObject[] FilterTargets(GameObject[] enemies)
    {
        GameObject[] myEnemies = new GameObject[1];
        if (target == null)
        {
            foreach (GameObject enemy in enemies)
            {
                if (CalcDistance(enemy) < getTowerRange())
                {
                    target = enemy;
                    break;
                }
            }
            if (target == null)
            {
                return null;
            }
        }
        Vector3 rotation = new Vector3();
        rotation = target.transform.position - gameObject.transform.position;
        rotation.y = 0;
        //rotation.z = 0;
        gameObject.transform.rotation = Quaternion.LookRotation(rotation);
        myEnemies[0] = target;
        return myEnemies;
    }

    protected override float GetDamage(GameObject enemy)
    {
        return attackDamage;
    }
}
