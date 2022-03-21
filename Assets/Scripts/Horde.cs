using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horde : MonoBehaviour
{
    public GameObject damageEffect;
    public float minSizeParticle = .4f;
    public float maxSizeParticle = 2f;
    public float particleScale = 1f;
    public float particleLifetime = 1f;
    public Material holoMat;

    private List<Material> materials;
    private Renderer[] enemyRenderers;

    //private float health = 100f;
    // Start is called before the first frame update
    void Awake()
    {
        //materials = new List<Material>();
        //enemyRenderers = GetComponentsInChildren<Renderer>();
        //for (int i = 0; i < enemyRenderers.Length; i++)
        //{
        //    materials.Add(enemyRenderers[i].material);
            
        //}
        //SwitchToHoloMat();
        //Invoke("SwitchToNormalMat", 5f);
                
    }

    void SwitchToHoloMat()
    {
        for (int i = 0; i < enemyRenderers.Length; i++)
        {
            enemyRenderers[i].material = holoMat;
        }
    }

    void SwitchToNormalMat()
    {
        for (int i = 0; i < enemyRenderers.Length; i++)
        {
            enemyRenderers[i].material = materials[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*internal void Damage(float damage)
    {
        health -= damage;
        Debug.Log($"Health {health}");
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
    internal void Damage(float damage, GameObject attackingTurret)
    {
        RunTakeDamageEffect(damage, attackingTurret);
        Damage(damage);
    }*/



    //At the moment, the only reason this script is used is for the particle effect.
    //However, the particle effect should probably be moved to the Tower's attack script since different towers cause different effects.
    public void RunTakeDamageEffect(float damage, GameObject attackingTurret)
    {

        GameObject particleObj = Instantiate(damageEffect, gameObject.transform.position,
            Quaternion.LookRotation(
                attackingTurret.transform.position - gameObject.transform.position));
        var main = particleObj.GetComponentInChildren<ParticleSystem>().main;
        main.startSize = Mathf.Min(Mathf.Max(particleScale * damage / 100,
            minSizeParticle), maxSizeParticle);
        ParticleSystem sys = particleObj.GetComponentInChildren<ParticleSystem>();
        Destroy(particleObj, particleLifetime);
    }

}
