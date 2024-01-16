using UnityEngine;
using UnityEngine.Rendering;

public class Turret : MonoBehaviour
{

    public float attackRange = 10f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public GameObject bulletPrefab;

    public float searchCountdown = 0.1f;
    private float currentSearchCountdown = 0f;
    public GameObject target;

    void Update()
    {
        if (currentSearchCountdown <= 0f)
        {
            target = FindNearestEnemy();
            currentSearchCountdown = searchCountdown;
        }

        currentSearchCountdown -= Time.deltaTime;


        if (target == null)
            return;

        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
        {
            if (fireCountdown <= 0f)
            {
                Shoot(target);
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    GameObject FindNearestEnemy()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy"))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = hitCollider.gameObject;
                }
            }
        }

        return nearestEnemy;
    }

    void Shoot(GameObject enemy)
    {
        GameObject bulletGO = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        AutoBullet bullet = bulletGO.GetComponent<AutoBullet>();

        bullet.Seek(enemy);
    }





    // GIZMOS //
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        //draw a line toward target
        if (target != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }
}
