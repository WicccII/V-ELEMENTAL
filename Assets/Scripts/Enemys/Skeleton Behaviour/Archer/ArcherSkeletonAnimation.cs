using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArcherSkeletonAnimation : MonoBehaviour
{
    Animator animator;
    EnemyStats enemyStats;
    ArcherSkeletonEnemeMove moveScript;
    float currentHealth;
    [Header("Modifile")]
    public GameObject projectile;
    public EnemyScriptableObject enemyData;
    public float coolDown;
    float timer;
    public float distance;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<EnemyStats>();
        currentHealth = enemyStats.currentHealth;
        moveScript = GetComponent<ArcherSkeletonEnemeMove>();
        AvailableAttack();
    }

    // Update is called once per frame
    void Update()
    {
        TakeDamage();
        StopMove();
        timer += Time.deltaTime;
        if (timer > coolDown)
        {
            AvailableAttack();
        }
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

    void StopMove()
    {
        if (IsPrepare())
        {
            moveScript.enabled = false;
            animator.SetBool("Move", false);
        }
        else
        {
            moveScript.enabled = true;
            animator.SetBool("Move", true);

        }
    }

    void AvailableAttack()
    {
        animator.SetBool("Attacked", true);
    }

    void FinishAttack()
    {
        animator.SetBool("Attacked", false);
        timer = 0;
    }

    private bool IsPrepare()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        float gapBetween = (transform.position - playerStats.transform.position).magnitude;
        if (gapBetween <= distance)
        {
            return true;
        }
        return false;
    }

    public void SpawnProjectile()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);

    }

}
