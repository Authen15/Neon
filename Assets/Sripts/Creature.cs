using System.Collections;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public float statMultiplier = 1f;

    public float maxHealth = 100;
    public float currentHealth = 100;

    public float speed = 5;
    
    private bool isFlashing = false;

    public SpriteRenderer _spriteRenderer;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        TakeDamageFeedback();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamageFeedback()
    {
        if (_spriteRenderer != null && !isFlashing)
        {
            StartCoroutine(ResetColor(0.1f, _spriteRenderer.color, _spriteRenderer));
        }
        else
        {
            Debug.Log("SpriteRenderer is null or creature is already flashing for " + transform.name + ".");
        }
    }

    private IEnumerator ResetColor(float waitTime, Color originalColor, SpriteRenderer spriteRenderer)
    {
        isFlashing = true;
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(waitTime);
        spriteRenderer.color = originalColor;
        isFlashing = false;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
