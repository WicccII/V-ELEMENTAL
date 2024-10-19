using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAnimation : MonoBehaviour
{
    Animator animator;
    EnemyStats enemyStats;
    public float currentHealth;
    public EnemyScriptableObject enemyData;
    float currentDamage;
    float takedameHealth;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<EnemyStats>();
        currentHealth = enemyStats.currentHealth;
        takedameHealth = enemyStats.currentHealth;
        currentDamage = enemyData.Damage;
      
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyStats.currentHealth < currentHealth)
        {
            animator.SetBool("Takedamage", true);
            currentHealth = enemyStats.currentHealth;
        }else
        {
            animator.SetBool("Takedamage", false);
        }
 
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            PlayerStats playerStats = FindObjectOfType<PlayerStats>();
            playerStats.TakeDamage(currentDamage);
        }
    }
}
