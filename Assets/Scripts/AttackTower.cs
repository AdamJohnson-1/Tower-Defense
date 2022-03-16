using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackTower : MonoBehaviour
{
    [Header("Effect Settings")]
    public GameObject damageEffect;
    public bool damageEffectEnabled = true;
    public float minSizeParticle = .4f;
    public float maxSizeParticle = 2f;
    public float particleScale = 1f;
    public float particleLifetime = 1f;

    protected float countUpToShoot = 0;
    public abstract float GetDamage(GameObject enemy);

    //Returns a float, example 2.5 seconds
    public abstract float AttackDelay();

    //This is called when the tower attacks or does it's special effect
    public virtual void Animate(List<GameObject> targetedEnemies)
    {
        //Does nothing by default
        //When a tower has an animation this method should be overriden
    }

    //returns only the enemies upon which this particular tower will have an effect
    public abstract List<GameObject> FilterTargets(List<GameObject> enemies);


    public void Update()
    {
        countUpToShoot += Time.deltaTime;
        if (countUpToShoot > AttackDelay())
        {
            var enemies = GameObject.FindGameObjectsWithTag("horde");
            var filteredEnemies = FilterTargets(new List<GameObject> (enemies));
            Animate(filteredEnemies);
            foreach (GameObject enemy in filteredEnemies)
            {
                Debug.Log("Shooting enemy");
                float damage = GetDamage(enemy);
                RunTakeDamageEffect(damage, enemy);
                enemy.GetComponent<MobScript>().TakeDamage(damage);
            }
        countUpToShoot = 0;
        }
    }

    public void RunTakeDamageEffect(float damage, GameObject enemy)
    {
        if (damageEffectEnabled)
        {

            GameObject particleObj = Instantiate(damageEffect, enemy.transform.position,
            Quaternion.LookRotation(
                gameObject.transform.position - enemy.transform.position));
            var main = particleObj.GetComponentInChildren<ParticleSystem>().main;
            main.startSize = Mathf.Min(Mathf.Max(particleScale * damage / 100,
                minSizeParticle), maxSizeParticle);
            //ParticleSystem sys = particleObj.GetComponentInChildren<ParticleSystem>();
            Destroy(particleObj, particleLifetime);
        }
    }




    public float CalcDistance(GameObject enemy)
    {
        Vector3 towerPosition = gameObject.transform.position;
        Vector3 enemyPosition = enemy.gameObject.transform.position;

        return Vector3.Distance(towerPosition, enemyPosition);
    }


    //Used solely for the interface when placing a tower. 
    public abstract float GetDefaultRange();


    //The price an implementation of the tower can be purchased for
    public abstract int GetTowerPrice();

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, GetDefaultRange());
    }


}
