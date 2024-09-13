using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerScriptableObject playerData;
    float currentHealth;
    float currentSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentRecovery;

    [Header("Level/Experience")]
    public int level = 1;
    public int experience = 0;
    public int experienceCap = 100;
    public int experienceCapIncrease;

    //I-frame
    [Header("I-frame")]
    public float invisibilityDuration;
    float invisibilityTimer;
    bool isInvincible;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = playerData.MaxHealth;
        currentSpeed = playerData.MoveSpeed;
        currentMight = playerData.Might;
        currentProjectileSpeed = playerData.ProjectileSpeed;
        currentRecovery = playerData.Recovery;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            if (invisibilityTimer > 0)
            {
                invisibilityTimer -= Time.deltaTime;
            }
            else
            {
                isInvincible = false;
            }
        }
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }

    public void LevelUpChecker()
    {
        if (experience >= experienceCap)//level up if experience out of cap
        {
            experience -= experienceCap;//remaining ex
            level++;//level up
            experienceCap += experienceCapIncrease;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            invisibilityTimer = invisibilityDuration;
            isInvincible = true;
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    void Kill()
    {
        Debug.Log("Player dead");
    }
}
