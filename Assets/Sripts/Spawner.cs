using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Ennemies enemiesScript;
    public int gameTime = 0; // game time in seconds
    private float spawnCountdown;
    public float spawnDelayMin = 10f;
    public float spawnDelayMax = 15f;
    public float spawnRadius = 30f; // radius around the spawner where enemies can spawn

    public int debugMult = 1;

    void Update()
    {
        gameTime = (int)Time.timeSinceLevelLoad * (int)debugMult;
        int gameLevel = CalculateGameLevel(gameTime);

        spawnCountdown -= Time.deltaTime;
        if (spawnCountdown <= 0f)
        {
            List<GameObject> enemiesWave = GetEnemiesWave(gameLevel);
            foreach (GameObject enemy in enemiesWave)
            {
                SpawnEnemy(enemy);
            }
            spawnCountdown = Random.Range(spawnDelayMin, spawnDelayMax);
        }
    }

    private int CalculateGameLevel(int gameTime)
    {
        // increase game level every minute
        return gameTime / 60 + 1;
    }

    private List<GameObject> GetEnemiesWave(int gameLevel)
    {
        int budget = GetEnemiesBudget(gameLevel);
        List<GameObject> enemiesWave = new List<GameObject>();

        while (budget > 0)
        {
            Ennemies.EnnemyLevel level = DetermineEnemyLevel(gameLevel);
            Ennemies.EnnemyType type = DetermineEnemyType();
            int cost = CalculateEnemyCost(level, type);

            if (budget >= cost)
            {
                GameObject enemy = enemiesScript.CreateEnemy(level, type);
                enemiesWave.Add(enemy);
                budget -= cost;
            }
            else
            {
                break;
            }
        }

        return enemiesWave;
    }

    private int GetEnemiesBudget(int gameLevel)
    {
        return gameLevel * 10;
    }

    private Ennemies.EnnemyLevel DetermineEnemyLevel(int gameLevel)
    {
        return (Ennemies.EnnemyLevel)Random.Range(1, Mathf.Min(gameLevel, (int)Ennemies.EnnemyLevel.LEVEL_6));
    }

    private Ennemies.EnnemyType DetermineEnemyType()
    {
        return (Ennemies.EnnemyType)Random.Range(1, 3);
    }

    private int CalculateEnemyCost(Ennemies.EnnemyLevel level, Ennemies.EnnemyType type)
    {
        return (int)level * (int)type;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Vector2 spawnPosition = Random.onUnitSphere * spawnRadius;
        enemy.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0) + transform.position;
    }
}
