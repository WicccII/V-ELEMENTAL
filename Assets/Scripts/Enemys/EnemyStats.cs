using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public BatMove batMove;
    public EnemyScriptableObject enemyData;
    [HideInInspector]
    public float currentHealth;
    float currentSpeed;
    float currentDamage;

    // Start is called before the first frame update
    void Awake()
    {
        batMove = GetComponent<BatMove>();
        currentHealth = enemyData.Health;
        currentSpeed = enemyData.Speed;
        currentDamage = enemyData.Damage;
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        batMove.enabled = false;
        new WaitForSeconds(1);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            kill();
        }
        else
        {
            batMove.enabled = true;
        }
    }

    void kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            playerStats.TakeDamage(currentDamage);
        }
    }
}
