using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public TextMeshProUGUI WaveTimerText;

    public EnemyManager enemyManager;
    public float _spawnRadius = 20.0f;
    private int difficultyLevel = 1;
    private float timeSinceLastWave = 0.0f;
    private float timeForNextWave = 5.0f;

    private int _waveCount = 1;

    private void Start()
    {
        WaveTimerText = GameObject.Find("WaveTimerText").GetComponent<TextMeshProUGUI>();
        enemyManager = GetComponent<EnemyManager>();

        SpawnWave();// spawn first wave
    }

    void Update()
    {
        timeSinceLastWave += Time.deltaTime;

        WaveTimerText.text = "Next wave in " + (int)(timeForNextWave - timeSinceLastWave) + " seconds";

        if (timeSinceLastWave >= timeForNextWave)
        {
            AdjustDifficulty();
            SpawnWave();
            timeSinceLastWave = 0.0f;
        }

        if(Input.GetKeyDown(KeyCode.I))
            enemyManager.SpawnEnemies(100, 1, transform.position, _spawnRadius); // spawn 10 enemies
        if(Input.GetKeyDown(KeyCode.J))
            enemyManager.SpawnEnemies(1000, 1, transform.position, _spawnRadius); // spawn 100 enemies
    }

    private void SpawnWave()
    {
        int waveBudget = CalculateWaveBudget();
        enemyManager.SpawnEnemies(waveBudget, difficultyLevel, transform.position, _spawnRadius);
        _waveCount++;
    }

    private int CalculateWaveBudget()
    {
        return difficultyLevel*difficultyLevel * 100 + _waveCount * 20 * difficultyLevel*difficultyLevel;
    }

    private void AdjustDifficulty()
    {
        // upgrade difficulty every half minute
        if(difficultyLevel < 6)
            difficultyLevel = (int)(Time.timeSinceLevelLoad / 30.0f) + 1;
    }
}
