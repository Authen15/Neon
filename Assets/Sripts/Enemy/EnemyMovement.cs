using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject _target;
    private EnemyCreature _enemyCreature;


    // Start is called before the first frame update
    void Start(){
        _target = GameObject.FindGameObjectWithTag("Player");
        _enemyCreature = GetComponent<EnemyCreature>();
    }

    void Update()
    {
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        Vector3 directionToTarget = _target.transform.position - transform.position;
        directionToTarget.Normalize();
        // transform.position += directionToTarget * speed * Time.deltaTime;
        transform.Translate(directionToTarget * _enemyCreature.speed * Time.deltaTime);
    }

}
