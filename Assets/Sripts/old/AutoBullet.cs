using UnityEngine;

public class AutoBullet : MonoBehaviour
{
    public int attackDamage = 10;
    public float speed = 5f;

    private GameObject target;

    // Init the bullet with a target
    public void Seek(GameObject _target)
    {
        target = _target;
    }

    void Update()
    {
        // if target does not exist anymore, destroy the bullet
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        // calculate the direction to the target
        Vector3 directionToTarget = target.transform.position - transform.position;
        directionToTarget.Normalize(); // Normaliser la direction

        // calculate the distance travelled this frame
        float stepSize = speed * Time.deltaTime;

        // move towards the target
        transform.position += directionToTarget * stepSize;

        // check if we have reached the target
        if (Vector3.Distance(transform.position, target.transform.position) <= stepSize)
        {
            HitTarget(target.GetComponent<Creature>());
        }
    }

    void HitTarget(Creature creature)
    {
        creature.TakeDamage(attackDamage);
        Destroy(gameObject);
    }
}
