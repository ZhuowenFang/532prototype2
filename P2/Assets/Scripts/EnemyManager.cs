using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float timeBetweenSpawns = 1f; // 默认生成时间
    float currentTimeBetweenSpawns;

    Transform enemiesParent;

    public static EnemyManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        enemiesParent = GameObject.Find("Enemies").transform;
    }

    void Update()
    {   
        if(!WaveManager.instance.WaveRuning())
        {
            return;
        }
        currentTimeBetweenSpawns -= Time.deltaTime;
        if(currentTimeBetweenSpawns <= 0)
        {
            SpawnEnemy();
            currentTimeBetweenSpawns = timeBetweenSpawns;
        }
    }

    public void SetSpawnTime(float newSpawnTime)
    {
        timeBetweenSpawns = newSpawnTime;
    }

    void SpawnEnemy()
    {
        var enemy = Instantiate(enemyPrefab, RandomPosition(), Quaternion.identity);
        enemy.transform.SetParent(enemiesParent);
    }

    Vector2 RandomPosition()
    {
        float randomX = Random.Range(-16f, 16f);
        float randomY = Random.Range(-8f, 8f);
        return new Vector2(randomX, randomY);
    }

    public void RemoveEnemies()
    {
        foreach(Transform child in enemiesParent)
        {
            Destroy(child.gameObject);
        }
    }
}

