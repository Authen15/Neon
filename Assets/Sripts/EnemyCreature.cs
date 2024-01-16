using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreature : Creature
{
    private GameObject target;
    private EnemyStats enemyStats;

    private float attackCountdown = 0f;

    // Start is called before the first frame update
    void Awake(){
        target = GameObject.FindGameObjectWithTag("Player");
        creatureStats = new EnemyStats();
        enemyStats = (EnemyStats)creatureStats;
        creatureStats.currentHealth = creatureStats.maxHealth;
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= enemyStats.attackRange)
        {
            if (attackCountdown <= 0f)
            {
                Attack();
                attackCountdown = 1f / enemyStats.attackSpeed;
            }

            attackCountdown -= Time.deltaTime;
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    //set the level of the enemy and update the stats
    public void SetLevel(int level)
    {
        creatureStats.level = level;
        UpdateStats();
    }

    private void UpdateStats()
    {
        enemyStats.maxHealth *= creatureStats.level;
        enemyStats.currentHealth = creatureStats.maxHealth;

        enemyStats.speed *= creatureStats.level;
        // enemyStats.attackRange *= creatureStats.level;
        enemyStats.attackDamage *= creatureStats.level;
        // enemyStats.attackSpeed *= creatureStats.level;
    }

    void MoveTowardsTarget()
    {
        Vector3 directionToTarget = target.transform.position - transform.position;
        directionToTarget.Normalize();

        // transform.position += directionToTarget * speed * Time.deltaTime;

        transform.Translate(directionToTarget * creatureStats.speed * Time.deltaTime);
    }

    void Attack(){
        target.GetComponent<Creature>().TakeDamage(enemyStats.attackDamage);
    }

}
