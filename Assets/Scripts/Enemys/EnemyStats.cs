using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    [HideInInspector]
    public float currentHealth;
    float currentSpeed;
    float currentDamage;
    MonoBehaviour enemyMoveScript;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = enemyData.Health;
        currentSpeed = enemyData.Speed;
        currentDamage = enemyData.Damage;

        enemyMoveScript = GetComponents<MonoBehaviour>().FirstOrDefault(script => script.GetType().Name.EndsWith("EnemyMove"));
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        if (enemyMoveScript != null)
        {
            enemyMoveScript.enabled = false;
        }

        StartCoroutine(EnableMoveAfterDelay(1f));
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            kill();
        }
    }

    IEnumerator EnableMoveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (currentHealth > 0 && enemyMoveScript != null)
        {
            enemyMoveScript.enabled = true;
        }
    }

    void kill()
    {
        Destroy(gameObject);
    }
}
