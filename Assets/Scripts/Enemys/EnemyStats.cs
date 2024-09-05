using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    float currentHealth;
    float currentSpeed;
    float currentDamage;
    // Start is called before the first frame update
    void Awake ()
    {
        currentHealth = enemyData.Health;
        currentSpeed = enemyData.Speed;
        currentDamage = enemyData.Damage;
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            kill();
        }
    }

    void kill()
    {
        Destroy(gameObject);
    }
}
