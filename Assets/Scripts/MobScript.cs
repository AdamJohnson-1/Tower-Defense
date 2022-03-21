using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class MobScript : MonoBehaviour
{
    //when towers want to check if they should continue firing at this target.
    public bool IsAlive = true;

    //how many lives are lost when this unit reaches the base
    public int BaseLivesLostOnArrival = 5;
    public int ScoreWorth = 10;
    public int MoneyWorth = 10;

    public float StartingHealth = 100f;
    private float Health;

    public float DefaultMoveSpeed = 3.5f;

    public NavMeshAgent navMeshAgent;
    private Vector3 destination;

    public GameObject healthBar;
    private HealthBarScript healthBarScript;

    void Start()
    {
        Health = StartingHealth;

        healthBarScript = healthBar.GetComponent<HealthBarScript>();

        //set pathfinder script's movespeed to BaseMoveSpeed
        navMeshAgent.speed = DefaultMoveSpeed;

        //set destination
        destination = GameObject.FindGameObjectWithTag("Destination").transform.position;
    }

    void Update()
    {
        //here we check if we've reached the base
        float distance = Vector3.Distance(destination, transform.position);
        if(distance < 4)
        {
            OnArriveToBase();
        }
    }


    public void TakeDamage(float amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            Cache.incrementScore(ScoreWorth);
            Shop.changeMoney(MoneyWorth);
            Die();
        }
        else
        {
            //update the health bar
            healthBarScript.SetCurrentHealth(Health);
        }
    }

    private void Die()
    {
        Health = 0;
        IsAlive = false;

        Destroy(gameObject);
        //destroy object logic should be in the EnemyHandler
        //we should call the enemyHandler function from here
    }

    private void OnArriveToBase()
    {
        //call SubtractLives on base object.
        GameBaseScript.subtractLives(BaseLivesLostOnArrival);
        Die();
    }

    //maybe add this later? For slowing effects or damage-over-time effects
    //public AddEffect(lambda, duration, frequency)

    public float GetStartingHealth()
    {
        return StartingHealth;
    }
}
