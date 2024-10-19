using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSwordSkeletonAnimation : MonoBehaviour
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
        timer += Time.deltaTime;
        TakeDamage();
        if(attackable.CheckIsAttackable() && timer > coolDown)
        {
            AvailableAttack();
            moveScript.enabled = false;
            timer = 0;
            attackable.attack = false;
        }
    }

    void TakeDamage()
    {
        if (enemyStats.currentHealth < currentHealth)
        {
            animator.SetBool("Hurt", true);
            FinishAttack();
            disableHitBox();
            currentHealth = enemyStats.currentHealth;
        }
        else
        {
            animator.SetBool("Hurt", false);
        }
    }
    
    public void AvailableAttack()
    {
        animator.SetBool("Attacked", true);
    }

    public void FinishAttack()
    {
        animator.SetBool("Attacked", false);
        moveScript.enabled = true;
    }

    public void enableHitBox()
    {
        hitBox.enabled = true;
    }

    public void disableHitBox()
    {
        hitBox.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") && hitBox.enabled)
        {
            collider.GetComponent<PlayerStats>().TakeDamage(enemyData.Damage);
        }
    }
}
