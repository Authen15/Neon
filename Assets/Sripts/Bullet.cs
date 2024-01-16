using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int attackDamage = 10;
    // public float speed = 5f;
    public float force = 10f;

    private ParticleSystem ps;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * force, ForceMode2D.Impulse);
        ps = GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Creature>().TakeDamage(attackDamage);
            ps.Play();
            rb.velocity = Vector2.zero;
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, ps.main.duration);
        }
    }
}
