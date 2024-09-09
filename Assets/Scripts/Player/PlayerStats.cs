using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerScriptableObject playerData;
    float currentHealth;
    float currentSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentRecovery;
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
        
    }
}
