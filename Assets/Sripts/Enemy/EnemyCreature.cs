using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreature : Creature
{
    private GameObject _player;
    private EnemyJobMove _jobMoveManager;

    public float _attackRange = 1f;
    public float _attackDamage = 5;
    public float _attackSpeed = 1f;

    private float _attackCountdown = 0f;

    public int _level = 1;

    private bool attack = false;


    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        _jobMoveManager = FindObjectOfType<EnemyJobMove>();
        _jobMoveManager.RegisterEnemy(this);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            attack = !attack;
        } 
        
        if(attack)
        {
            float distanceToTarget = Vector3.Distance(transform.position, _player.transform.position);
            if (distanceToTarget <= _attackRange)
            {
                if (_attackCountdown <= 0f)
                {
                    Attack();
                    _attackCountdown = 1f / _attackSpeed;
                }

                _attackCountdown -= Time.deltaTime;
            }
        }

    }

    public void SetLevel(int level)
    {
        this._level = level;
        UpdateStats();
    }



    private void UpdateStats()
    {
        maxHealth *= _level;
        currentHealth = maxHealth;
        _attackDamage *= _level;

        // enemyStats.speed *= creatureStats.level;
        // enemyStats.attackRange *= creatureStats.level;
        // enemyStats.attackSpeed *= creatureStats.level;
    }

    void Attack(){
        _player.GetComponent<Creature>().TakeDamage(_attackDamage);
    }

    protected override void Die()
    {
        // Destroy(gameObject);
        gameObject.SetActive(false);
        _jobMoveManager.UnregisterEnemy(this);
        _player.GetComponent<PlayerCreature>().GainExperience(_level*_level);
    }

}
