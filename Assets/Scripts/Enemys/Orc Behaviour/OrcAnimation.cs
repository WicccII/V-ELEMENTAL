using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    EnemyStats orcStats;
    public float currentHealth;
    float takedameHealth;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        orcStats = GetComponent<EnemyStats>();
        currentHealth = orcStats.currentHealth;
        takedameHealth = orcStats.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (orcStats.currentHealth < currentHealth)
        {
            animator.SetBool("Takedamage", true);
            currentHealth = orcStats.currentHealth;
        }
        else
        {
            animator.SetBool("Takedamage", false);
        }
    }
}
