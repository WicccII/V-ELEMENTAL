using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleSkillBehaviour : SkillController
{
    public float offset;
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
        collider2D.enabled = false;
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

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
        float dirx = direction.x;

        // Get current scale and position
        Vector3 scale = transform.localScale;
        Vector3 transformPosition = transform.position;

        if (dirx < 0) // Moving left
        {
            scale.x = Mathf.Abs(scale.x) * -1;  // Flip scale to the left
            transformPosition.x = playerMove.transform.position.x - offset;  // Flip position to the left
        }
        else if (dirx > 0) // Moving right
        {
            scale.x = Mathf.Abs(scale.x);  // Reset scale to face right
            transformPosition.x = playerMove.transform.position.x + offset;  // Reset position to the right
        }

        // Apply the new scale and position
        transform.localScale = scale;
        transform.position = transformPosition;
    }
}
