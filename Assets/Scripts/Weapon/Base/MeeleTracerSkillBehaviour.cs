using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleTracerSkillBehaviour : SkillController
{
    public int rotation;
    protected Vector3 direction;
    //current stats
    protected float currentDamage;
    float currentSpeed;
    float currentPierce;
    float currentKnockBackForce;
    Animator animator;
    new Collider2D collider2D;
    void Awake()
    {
        currentDamage = skillData.Damage;
        currentSpeed = skillData.Speed;
        currentPierce = skillData.Pierce;
        currentKnockBackForce = skillData.KnockBackForce;
    }

    public float GetCurrentDamage()
    {
        return currentDamage * FindObjectOfType<PlayerStats>().CurrentMight;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;
        UpdateDirection();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        base.Attack();
        AvailableAttack();
    }

    public void AvailableAttack()
    {
        animator.SetBool("Attacked", true);
        collider2D.enabled = true;
    }

    protected virtual void FinishAttack()
    {
        animator.SetBool("Attacked", false);
        collider2D.enabled = false; ;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //if collision with enemy then take damage
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            Transform playerTranform = FindObjectOfType<PlayerStats>().transform;
            Vector3 direction = (enemyStats.transform.position - playerTranform.position).normalized;
            enemyStats.TakeDamage(GetCurrentDamage());
            enemyStats.GetComponent<Rigidbody2D>().AddForce(direction * currentKnockBackForce, ForceMode2D.Impulse);
        }
        else if (collision.CompareTag("Prop"))
        {
            if (collision.TryGetComponent(out BreakableProps breakableProps))
            {
                breakableProps.TakeDamage(GetCurrentDamage());
            }
        }
    }

    public void UpdateDirection()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure Z-axis is zero for 2D

        // Calculate direction from the projectile to the mouse cursor
        direction = (mousePosition - transform.position).normalized;

        // Rotate the projectile to face the mouse direction and adjust by -90 degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - rotation;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

}
