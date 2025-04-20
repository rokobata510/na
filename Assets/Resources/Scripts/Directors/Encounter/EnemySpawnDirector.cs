using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnDirector : AHasReferenceToEncounterDirector
{
    private int availablePointsCopy;
    public List<SpawnPoint> spawnPoints;
    public override void SetupFields(EncounterDirectorContainer directorContainer)
    {
        base.SetupFields(directorContainer);
        availablePointsCopy = encounter.spawnPoints;
    }
    public override void StartDirector()
    {
        FindSpawnPoints();
        AssignPoints();
        SpawnEnemies();
        RemoveSpawnPoints();
    }
    public override void UpdateDirector()
    {
    }
    
    void FindSpawnPoints()
    {
        this.spawnPoints = new List<SpawnPoint>();
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (GameObject spawnPoint in spawnPoints)
        {
            this.spawnPoints.Add(spawnPoint.GetComponent<SpawnPoint>());
        }
    }
    public void SpawnEnemies()
    {
        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            spawnPoint.SpawnAll();
        }
    }




    private void AssignPoints()
    {
        int circuitBreaker = 1000;
        while (availablePointsCopy > 0 && circuitBreaker > 0)
        {
            circuitBreaker--;
            foreach (SpawnPoint spawnPoint in spawnPoints)
            {
                if (availablePointsCopy <= 0)
                {
                    break;
                }
                int points = 1;
                availablePointsCopy -= points;
                spawnPoint.availablePoints += points;
           }
        }
        if (circuitBreaker <= 0)
        {
            Debug.LogError("Circuit breaker reached");
        }
    }

    private void RemoveSpawnPoints()
    {
        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            Destroy(spawnPoint.gameObject);
        }
        spawnPoints = new();
    }


}