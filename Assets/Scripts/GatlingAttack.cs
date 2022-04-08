using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingAttack : AttackTower
{
    [Header("Gatling Settings")]
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
            Vector3 rotation;
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
        List<GameObject> enemiesToAttack = new AttackFastestStrategy(enemies).selectTargets(this);
        if (enemiesToAttack.Count > 0)
        {
            target = enemiesToAttack[0];
        }
        else
        {
            target = null;
        }
        return enemiesToAttack;
    }

    // public override AudioSource GetAttackSoundEffect()
    // {
    //     return attackSoundEffect;
    // }

}
