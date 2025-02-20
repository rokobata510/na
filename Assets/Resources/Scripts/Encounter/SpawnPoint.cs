using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public List<GameObject> spawnableEnemies = new();
    public int availablePoints;
    private void Start()
    {
        if (spawnableEnemies.Exists(enemy => !enemy.TryGetComponent<EnemyAI>(out _)))
        {
            throw new Exception("Spawnable enemies must have an AEnemyAI script attached!");
        }
    }

    public int SpawnAll()
    {
        int circuitBreaker = 1000;
        while (availablePoints > 0&& circuitBreaker>0)
        {
            circuitBreaker--;
            availablePoints -= SpawnOne(availablePoints);
        }
        if (circuitBreaker == 0)
        {
            throw new Exception("Infinite loop detected in SpawnAll!");
        }
        return availablePoints;
    }
    int SpawnOne(int availablePoints)
    {

        if (availablePoints <= 0)
        {
            return int.MaxValue;
        }

        if (spawnableEnemies.Count == 0)
        {
            return int.MaxValue;
        }

        GameObject enemy = spawnableEnemies[EncounterRandomStream.Range(0, spawnableEnemies.Count)];
        if (!enemy.TryGetComponent<EnemyAI>(out var enemyAI))
        {
            throw new Exception("Spawnable enemies must have an AEnemyAI script attached!");
        }
        float maxHorizontalOffset = transform.lossyScale.x / 2;
        float maxVerticalOffset = transform.lossyScale.y / 2;
        UnnormalizedVector3 spawnPosition = new(transform.position.x + EncounterRandomStream.Range(-maxHorizontalOffset, maxHorizontalOffset), transform.position.y + EncounterRandomStream.Range(-maxVerticalOffset,maxVerticalOffset));
        Instantiate(enemy, spawnPosition, Quaternion.identity);

        if (availablePoints < enemyAI.spawnCost)
        {
            return int.MaxValue;
        }

        return enemyAI.spawnCost;

    }
}