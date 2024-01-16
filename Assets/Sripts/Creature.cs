using System.Collections;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    protected BaseStats creatureStats;
    private bool isFlashing = false;

    public void TakeDamage(float damage)
    {
        creatureStats.currentHealth -= damage;
        TakeDamageFeedback();

        if (creatureStats.currentHealth <= 0f)
        {
            Die();
        }
    }

    public virtual void TakeDamageFeedback()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && !isFlashing)
        {
            StartCoroutine(ResetColor(0.1f, spriteRenderer.color, spriteRenderer));
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

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
