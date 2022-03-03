using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaTower : AttackTower
{
    public float towerRange = 20.0f;
    public float maxDamage = 150f;
    public float minDamage = 1f;

    public float lightOnMaxTime = .1f;
    public ParticleSystem lightningSystem;
    private ParticleSystem[] systems;
    //private Boolean playing;
    private Light teslaBulb;
    private float lightOnTime;



    // Update is called once per frame
    // I am programming the towers to periodically
    // send out a massive shock. Kind of like an EMP shockwave.
    // Perhaps the strength should decrease with the inverse cube of the distance

    private void Awake()
    {
        systems = gameObject.GetComponentsInChildren<ParticleSystem>();
        teslaBulb = gameObject.GetComponentInChildren<Light>();
        lightOnTime = 0f;
        teslaBulb.range = towerRange;
        countUpToShoot = AttackDelay(); // allow tower to shoot right after placement
    }

    new void Update()
    {
        base.Update();

        if (lightOnTime < lightOnMaxTime)
        {
            lightOnTime += Time.deltaTime;
        }
        else
        {
            teslaBulb.enabled = false;
            lightOnTime = 0f;
        }
    }


    public override float GetDamage(GameObject enemy)
    {
        var damage = (float)(maxDamage / Math.Pow(CalcDistance(enemy) + 1, 2));
        return Math.Max(minDamage, damage);
    }

    public override float AttackDelay()
    {
        return 0.35f;
    }

    public override List<GameObject> FilterTargets(List<GameObject> enemies)
    {
        var en = new List<GameObject> (enemies);
        return en.FindAll(h => CalcDistance(h) < towerRange);
    }

    public override void Animate(List<GameObject> targetedEnemies)
    {
        foreach (ParticleSystem system in systems)
        {
            lightOnTime = 0f; //reset the light turn off countdown
            teslaBulb.enabled = true;
            system.Play();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, towerRange);
    }

    public override float GetDefaultRange()
    {
        return towerRange;
    }

    public override int GetTowerPrice()
    {
        return 60;
    }
}
