using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int _attackDamage = 10;
    public float _speed = 10f;
    
    private Rigidbody2D _rb;
    private ParticleSystem _ps;

    private bool _isHit = false;

    private float _lifeTime = 5f;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;
        _ps = GetComponent<ParticleSystem>();

    }

    void Update()
    {
        if(!_isHit){
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        if(_lifeTime <= 0){
            Destroy(gameObject);
        }
        _lifeTime -= Time.deltaTime;
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Enemy"))
    //     {
    //         collision.gameObject.GetComponent<Creature>().TakeDamage(_attackDamage);
    //         _ps.Play();
    //         // rb.velocity = Vector2.zero;
    //         GetComponent<SpriteRenderer>().enabled = false;
    //         Destroy(gameObject, _ps.main.duration);
    //     }
    // }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Creature>().TakeDamage(_attackDamage);
            _isHit = true;

            _ps.Play();
            DisableComponents();
            Destroy(gameObject, _ps.main.duration);
        }
    }

    private void DisableComponents()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        _rb.velocity = Vector2.zero;
    }
}
