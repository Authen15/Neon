using UnityEngine;

public class Ennemies : MonoBehaviour
{
    public GameObject enemyPrefab; // Assurez-vous d'assigner un préfabriqué d'ennemi basique

    public enum EnnemyLevel
    {
        LEVEL_1 = 1,
        LEVEL_2 = 2,
        LEVEL_3 = 3,
        LEVEL_4 = 4,
        LEVEL_5 = 5,
        LEVEL_6 = 6
    }

    public enum EnnemyType
    {
        TRIANGLE = 1,
        SQUARE = 2
    }

    public GameObject CreateEnemy(EnnemyLevel level, EnnemyType type)
    {
        GameObject enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);

        ConfigureEnemy(enemy, level, type);

        return enemy;
    }

    private void ConfigureEnemy(GameObject enemy, EnnemyLevel level, EnnemyType type)
    {
        // Configure stats depending on level
        enemy.GetComponent<EnemyCreature>().SetLevel((int)level);

        // Configure color depending on level
        SpriteRenderer sr = enemy.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = GetColorByLevel(level);
        }
    }

    Color GetColorByLevel(EnnemyLevel level)
    {
        switch (level)
        {
            case EnnemyLevel.LEVEL_1: return Color.green;
            case EnnemyLevel.LEVEL_2: return Color.blue;
            case EnnemyLevel.LEVEL_3: return Color.magenta;
            case EnnemyLevel.LEVEL_4: return Color.red;
            case EnnemyLevel.LEVEL_5: return Color.yellow;
            case EnnemyLevel.LEVEL_6: return Color.black;

            default: return Color.white;
        }
    }
}
