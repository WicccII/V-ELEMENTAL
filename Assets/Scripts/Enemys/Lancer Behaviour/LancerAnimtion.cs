using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerAnimtion : MonoBehaviour
{
    Animator animator;
    EnemyStats enemyStats;
    float currentHealth;
    public EnemyScriptableObject enemyData;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<EnemyStats>();
        currentHealth = enemyStats.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        TakeDamage();
    }

    void TakeDamage()
    {
        if (enemyStats.currentHealth < currentHealth)
        {
            animator.SetBool("Hurt", true);
            currentHealth = enemyStats.currentHealth;
        }
        else
        {
            animator.SetBool("Hurt", false);
        }
    }
}
