using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerAnimtion : MonoBehaviour
{
    Animator animator;
    EnemyStats enemyStats;
    GreatSwordskeletonEnemyMove moveScript;
    float currentHealth;
    public EnemyScriptableObject enemyData;
    public Collider2D hitBox;
    public Attackable attackable;
    public float coolDown;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<EnemyStats>();
        currentHealth = enemyStats.currentHealth;
        moveScript = GetComponent<GreatSwordskeletonEnemyMove>();
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
