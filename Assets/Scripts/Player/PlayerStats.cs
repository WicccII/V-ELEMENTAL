using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerScriptableObject playerData;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMagnet;
    [HideInInspector]
    public GameObject currentCharacter;


    //spawn StartingWeapon
    public List<GameObject> spawnWeapons;

    [Header("Level/Experience")]
    public int level = 1;
    public int experience = 0;
    public int baseCap = 100; // Initial experience cap for level 1
    public float experienceCapMultiplier = 1.5f; // Growth multiplier for experience
    public float levelReach; // Factor that increases with level

    //I-frame
    [Header("I-frame")]
    public float invisibilityDuration;
    float invisibilityTimer;
    bool isInvincible;

    // Start is called before the first frame update
    void Awake()
    {
        //Get charater choosing in menu
        playerData = CharacterSelecter.GetCharacter();

        currentHealth = playerData.MaxHealth;
        currentSpeed = playerData.MoveSpeed;
        currentMight = playerData.Might;
        currentProjectileSpeed = playerData.ProjectileSpeed;
        currentRecovery = playerData.Recovery;
        currentMagnet = playerData.Magnet;
        currentCharacter = playerData.Character;

        //spawn StartingWeapon
        SpawnWeapon(playerData.StartingWeapon);


        levelReach = GetExperienceCap();
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
        recover();
        levelReach = GetExperienceCap();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }

    public void LevelUpChecker()
    {
        while (experience >= GetExperienceCap()) // Check for level up
        {
            experience -= GetExperienceCap(); // Deduct the current cap from experience
            level++; // Level up
        }
    }

    public int GetExperienceCap()
    {
        // Calculate XP cap using exponential growth
        return (int)(baseCap * Mathf.Pow(experienceCapMultiplier, level - 1));
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

    public void IncreaseHealth(int amount)
    {
        if (currentHealth < playerData.MaxHealth)
        {
            currentHealth += amount;
        }
        else
        {
            currentHealth = playerData.MaxHealth;
        }
    }

    void recover()
    {
        if (currentHealth < playerData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;
        } 
        else
        {
            currentHealth = playerData.MaxHealth;
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        //startingWeapon
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform); //set child of player
        spawnWeapons.Add(spawnedWeapon); //add into list of weapons
    }
}
