using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float waitBetweenWaves;
    private float timePassed;
    private float countUpToNextWave;
    public float waveScaling = .5f;
    public Transform destination;

    public List<GameObject> enemies;
    public List<float> enemyDangerLvl;
    public List<int> enemyRarity;

    private Dictionary<GameObject, float> enemyToDangerLvl;


    // Start is called before the first frame update
    void Start()
    {
        if (enemies.Count != enemyDangerLvl.Count ||
            enemyDangerLvl.Count != enemyRarity.Count)
        {
            Debug.LogWarning("Enemy spawner lists (objects, danger level, and rarity) must be the same size");
        }
        enemyToDangerLvl = new Dictionary<GameObject, float>();
        for (int i = 0; i < enemies.Count; i++)  
        {
            enemyToDangerLvl.Add(enemies[i], enemyDangerLvl[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        countUpToNextWave += Time.deltaTime;
        if (countUpToNextWave >= waitBetweenWaves)
        {
            spawnEnemies(getWavePower());
            countUpToNextWave = 0;
        }
        
    }
    private float getWavePower()
    {
        float x = timePassed;
        return Mathf.Max(Mathf.Sin(x) - 4 * Mathf.Pow(Mathf.Cos(x), 2) + x/30 + (x%5)/30, 0f) * waveScaling + 1;
    }
    private void spawnEnemies(float wavePower)
    {
        while (wavePower > 0)
        {
            int rarityToGenerate = 0;
            while (Random.Range(0, 2) == 1 && rarityToGenerate < 10)
            {
                rarityToGenerate += 1;
            }
            List<GameObject> enemiesPossible = new List<GameObject>();
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemyDangerLvl[i] <= wavePower && enemyRarity[i] <= rarityToGenerate)
                {
                    enemiesPossible.Insert(0, enemies[i]);
                }
            }
            if (enemiesPossible.Count == 0) 
            {
                break;
            }
            int index = Random.Range(0, enemiesPossible.Count);
            GameObject enemyToSpawn = enemiesPossible[index];
            float wavePowerReduction;
            enemyToDangerLvl.TryGetValue(enemyToSpawn, out wavePowerReduction);
            wavePower -= wavePowerReduction;
            spawnEnemy(enemyToSpawn);
        }
        
    }
    private void spawnEnemy(GameObject enemy)
    {
        float positionOnEdge = Random.Range(.1f, 49.9f);
        Instantiate(enemy, new Vector3(57f, 0, positionOnEdge), Quaternion.Euler(new Vector3(0,0, 0)));
    }
}
