using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Unity.Burst;
using System.Collections.Generic;
using TMPro;

public class EnemyJobMove : MonoBehaviour
{
    private List<EnemyCreature> _enemies;
    private NativeList<Vector2> _nativePositions;
    private NativeList<float> _nativeSpeeds;
    private Transform _playerTransform;

    private JobHandle jobHandle;

    private TextMeshProUGUI _ennemyCountText;
    private int _ennemyCount;

    private Queue<EnemyCreature> _enemiesToRegister = new Queue<EnemyCreature>();
    private Queue<EnemyCreature> _enemiesToUnregister = new Queue<EnemyCreature>();

    void Awake()
    {
        _enemies = new List<EnemyCreature>();
        _nativePositions = new NativeList<Vector2>(Allocator.Persistent);
        _nativeSpeeds = new NativeList<float>(Allocator.Persistent);
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void RegisterEnemy(EnemyCreature enemy)
    {
        _enemiesToRegister.Enqueue(enemy);
    }

    public void UnregisterEnemy(EnemyCreature enemy)
    {
       _enemiesToUnregister.Enqueue(enemy);
    }

    void Update()
    {
        if (_nativePositions.Length != _enemies.Count & _nativePositions.Length != _enemies.Count)
        {
            Debug.LogError("Inconsistency between enemies and positions/speed count.");
            return;
        }

        Vector2 playerPosition = _playerTransform.position;
        EnemyJob enemyJob = new EnemyJob
        {
            TargetPosition = playerPosition,
            CurrentPositions = _nativePositions,
            Speeds = _nativeSpeeds,
            DeltaTime = Time.deltaTime
        };

        jobHandle = enemyJob.Schedule(_nativePositions.Length, 64);

        
    }

    void LateUpdate()
    {
        jobHandle.Complete();

        for (int i = 0; i < _enemies.Count -1; i++)
        {
            _enemies[i].transform.position = _nativePositions[i];
        }

        UpdateEnemyList();
    }

    private void UpdateEnemyList()
    {
        UpdateEnemyToUnregister();
        UpdateEnemyToRegister();
    }

    private void UpdateEnemyToRegister()
    {
        while (_enemiesToRegister.Count > 0)
        {
            EnemyCreature enemy = _enemiesToRegister.Dequeue();
            enemy.gameObject.SetActive(true);
            _enemies.Add(enemy);
            _nativePositions.Add(enemy.transform.position);
            _nativeSpeeds.Add(enemy.speed);

            _ennemyCount++;
        }
    }

    private void UpdateEnemyToUnregister()
    {
        while (_enemiesToUnregister.Count > 0)
        {
            EnemyCreature enemy = _enemiesToUnregister.Dequeue();
            int index = _enemies.IndexOf(enemy);
            if (index != -1)
            {
                Destroy(enemy.gameObject);
                _enemies.RemoveAt(index);
                _nativePositions.RemoveAt(index);
                _nativeSpeeds.RemoveAt(index);
                _ennemyCount--;

            }
        }
    }


    private void OnDestroy()
    {
        if (_nativePositions.IsCreated)
        {
            _nativePositions.Dispose();
        }
        if (_nativeSpeeds.IsCreated)
        {
            _nativeSpeeds.Dispose();
        }
    }

    private void OnGUI()
    {
        if (_ennemyCountText == null)
        {
            _ennemyCountText = GameObject.Find("EnemyCountText").GetComponent<TextMeshProUGUI>();
        }

        _ennemyCountText.text = "Enemy Count : " + _ennemyCount;
    }
}

[BurstCompile]
public struct EnemyJob : IJobParallelFor
{
    public Vector2 TargetPosition;
    public NativeArray<Vector2> CurrentPositions;
    public NativeArray<float> Speeds;
    public float DeltaTime;
    

    public void Execute(int index)
    {
        Vector2 directionToTarget = TargetPosition - CurrentPositions[index];
        directionToTarget.Normalize();

        Vector2 newPosition = CurrentPositions[index] + directionToTarget * DeltaTime * Speeds[index];
        CurrentPositions[index] = newPosition;
    }
}








