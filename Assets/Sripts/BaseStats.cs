using UnityEngine;

public abstract class BaseStats {
    public int level;
    public float statMultiplier = 1f;

    public float maxHealth = 100;
    public float currentHealth = 100;

    public float speed = 1f;

    public virtual void SetLevel(int level) {
        this.level = level;
        statMultiplier *= level;
    }
}
