using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerSkillBehaviour : MonoBehaviour
{
    [HideInInspector]
    protected Vector3 direction;
    public SkillScriptableObject weaponData;
    public float destroyTime;

    //current stats
    [HideInInspector]
    public float currentCooldown;
    [HideInInspector]
    public float currentDamage;
    [HideInInspector]
    public float currentSpeed;
    [HideInInspector]
    public float currentPierce;
    [HideInInspector]
    public float currentKnockBackForce;

    void Awake()
    {
        currentCooldown = weaponData.CooldownDuration;
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentPierce = weaponData.Pierce;
        currentKnockBackForce = weaponData.KnockBackForce;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyTime);
        UpdateDirection();
    }

    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }

    // Update direction based on mouse cursor
    public void UpdateDirection()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure Z-axis is zero for 2D

        // Calculate direction from the projectile to the mouse cursor
        direction = (mousePosition - transform.position).normalized;

        // Rotate the projectile to face the mouse direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //if collision with enemy then take damage
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(GetCurrentDamage());
            enemyStats.GetComponent<Rigidbody2D>().AddForce(direction * currentKnockBackForce, ForceMode2D.Impulse);
            ReducePierce();
        }
        else if (collision.CompareTag("Prop"))
        {
            if (collision.TryGetComponent(out BreakableProps breakableProps))
            {
                breakableProps.TakeDamage(GetCurrentDamage());
                ReducePierce();
            }
        }
    }

    void ReducePierce()
    {
        currentPierce--;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
