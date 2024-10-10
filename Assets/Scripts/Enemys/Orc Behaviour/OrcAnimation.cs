using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcAnimation : MonoBehaviour
{
    Animator animator;
    EnemyStats orcStats;
    public float currentHealth;
    float takedameHealth;
    private bool isInAttackRange = false;

    // Cooldown settings
    public float attackCooldown = 1f; // 1 second cooldown
    private float cooldownTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        orcStats = GetComponent<EnemyStats>();
        currentHealth = orcStats.currentHealth;
        takedameHealth = orcStats.currentHealth;
    }

    void Update()
    {
        // Handle the cooldown timer
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime; // Reduce timer over time
        }

        // Handle the Takedamage animation
        if (orcStats.currentHealth < currentHealth)
        {
            animator.SetBool("Takedamage", true);
            currentHealth = orcStats.currentHealth;
        }
        else
        {
            animator.SetBool("Takedamage", false);
        }

        // Only attack if in range and cooldown has expired
        if (isInAttackRange && cooldownTimer <= 0)
        {
            animator.SetBool("FindPlayer", true);
            cooldownTimer = attackCooldown; // Reset cooldown after attack
        }
        else
        {
            animator.SetBool("FindPlayer", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInAttackRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInAttackRange = false;
        }
    }
}
