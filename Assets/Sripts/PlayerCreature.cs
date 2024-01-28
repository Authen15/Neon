using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreature : Creature
{
    public TextMeshProUGUI levelText;
    public Image exprerienceBar;
    public float experience = 0;
    public float experienceToNextLevel = 20;
    public int level = 1;

    public float fireRate = 2f;

    void Start()
    {
        levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    public void GainExperience(int amount)
    {
        experience += amount;
        if (experience >= experienceToNextLevel)
        {
            level++;
            experience = 0;
            experienceToNextLevel *= 1.5f;
            UpdateStats();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        levelText.text = level.ToString();
        exprerienceBar.fillAmount = experience / experienceToNextLevel;
    } 

    private void UpdateStats()
    {
        maxHealth *= level;
        currentHealth = maxHealth;
        fireRate += level;
    }
}
