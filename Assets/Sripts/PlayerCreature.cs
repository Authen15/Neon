using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreature : Creature
{
    void Awake()
    {
        creatureStats = new PlayerStats();
        creatureStats.currentHealth = creatureStats.maxHealth;
    }

}
