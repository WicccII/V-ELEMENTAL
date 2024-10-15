using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerAnimtion : MonoBehaviour
{
    Animator animator;
    EnemyStats enemyStats;
    float currentHealth;
    public EnemyScriptableObject enemyData;
    float currentDamage;
    public float coolDown;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<EnemyStats>();
        currentHealth = enemyStats.currentHealth;
        currentDamage = enemyData.Damage;
        timer = coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        TakeDamage();
        if (timer > coolDown)
        {
            StartCoroutine(AvailableAttack());
        }
    }

    /// </summary>
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            PlayerStats playerStats = FindObjectOfType<PlayerStats>();
            playerStats.TakeDamage(currentDamage);
        }
    }

    private bool isPrepare()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        float gapBetween = (transform.position - playerStats.transform.position).magnitude;
        if (gapBetween <= 10)
        {
            return true;
        }
        return false;
    }

    private IEnumerator AvailableAttack()
    {
        if (isPrepare())
        {
            PlayerStats playerStats = FindObjectOfType<PlayerStats>();
            animator.SetBool("Attacked", true);
            enemyStats.GetComponent<Rigidbody2D>().AddForce((playerStats.transform.position - transform.position).normalized, ForceMode2D.Impulse);
            yield return new WaitForSeconds(1.5f);
            FinishAttack();
        }
    }

    private void FinishAttack()
    {
        animator.SetBool("Attacked", false);
        timer = 0;
    }
}
