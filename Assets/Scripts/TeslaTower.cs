using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaTower : MonoBehaviour
{
    public float shootDelay = 0.35f;
    private float thisTime = 0;
    public float towerRange = 20.0f;
    public float maxDamage = 150f;
    public float minDamage = 1f;
    public ParticleSystem lightningSystem;
    private ParticleSystem[] systems;
    private Boolean playing;



    // Update is called once per frame
    // I am programming the towers to periodically
    // send out a massive shock. Kind of like an EMP shockwave.
    // Perhaps the strength should decrease with the inverse cube of the distance

    private void Awake()
    {
        systems = gameObject.GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        thisTime += Time.deltaTime;
        //Debug.Log($"Tower.update() {thisTime} {lastTime} ");    
        if (thisTime > shootDelay) {
            SendShock();
            thisTime = 0;
        }
    }


    void SendShock()
    {
        playing = false;
        foreach (GameObject enemy in GetHorde()) {
            float dist = CalcDistance(enemy);
            if (dist < towerRange) {
                playing = true;
                Debug.Log("Shooting enemy");
                float damage = (float)(maxDamage / Math.Pow(dist + 1, 2));
                damage = Math.Max(minDamage, damage);
                enemy.GetComponent<Horde>().Damage(damage);
            }
        }
        foreach (ParticleSystem system in systems)
        {
            if (playing)
            {
                system.Play();
            }
            else
            {
                system.Stop();
            }
        }
    }

    private float CalcDistance(GameObject enemy)
    {
        Vector3 towerPosition = gameObject.transform.position;
        Vector3 enemyPosition = enemy.gameObject.transform.position;

        return Vector3.Distance(towerPosition, enemyPosition);
    }

    private GameObject[] GetHorde()
    {
        return GameObject.FindGameObjectsWithTag("horde");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, towerRange);
    }
}