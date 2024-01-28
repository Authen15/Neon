using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject _enemyPrefab;

    public void SpawnEnemies(int budget, int maxLevel, Vector3 spawnCenter, float spawnRadius)
    {
        int enemyLevel = maxLevel;
        while (budget > 0)
        {
            int cost = CalculateEnemyCost(enemyLevel);

            if (budget >= cost)
            {
                budget -= cost;
                SpawnEnemy(enemyLevel, spawnCenter, spawnRadius);
            }
            else
            {
                if(enemyLevel > 1)
                    enemyLevel--;
            }
        }
    }

    private int CalculateEnemyCost(int level)
    {
        return (int)Mathf.Pow(10, level);
    }

    private void SpawnEnemy(int level, Vector3 center, float radius)
    {
        Vector3 spawnPosition = RandomCirclePosition(center, radius);
        GameObject enemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.gameObject.SetActive(false);
        EnemyCreature enemyCreature = enemy.GetComponent<EnemyCreature>();

        if (enemyCreature != null)
        {
            AdjustEnemyStats(enemyCreature, level);
            AdjustEnemyColor(enemyCreature, level);
        }
    }

    private Vector3 RandomCirclePosition(Vector3 center, float radius)
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        Vector3 position;
        position.x = center.x + radius * Mathf.Cos(angle);
        position.y = center.y + radius * Mathf.Sin(angle);
        position.z = center.z;
        return position;
    }

    private void AdjustEnemyStats(EnemyCreature enemy, int level)
    {
        enemy.maxHealth += level * 10;
        enemy.currentHealth = enemy.maxHealth;
        enemy._attackDamage += level * 2;
        enemy._level = level;
    }

    private void AdjustEnemyColor(EnemyCreature enemyCreature, int level){
        switch (level)
        {
            case 1:
                enemyCreature._spriteRenderer.color = Color.green;
                break;
            case 2:
                enemyCreature._spriteRenderer.color = Color.blue;
                break;
            case 3:
                enemyCreature._spriteRenderer.color = Color.magenta;
                break;
            case 4:
                enemyCreature._spriteRenderer.color = Color.yellow;
                break;
            case 5:
                enemyCreature._spriteRenderer.color = Color.red;
                break;
            case 6:
                enemyCreature._spriteRenderer.color = Color.black;
                break;
        }
    }
}
