using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base script of all meele weapon
public class MeeleWeaponBehaviour : MonoBehaviour
{
    public SkillScriptableObject weaponData;
    public float destroyTime;
    Vector3 direction;
    ProjectileWeaponBehaviour projectileWeaponBehaviour;
    //current stats
    float currentCooldown;
    protected float currentDamage;
    float currentSpeed;
    float currentPierce;
    float currentKnockBackForce;

    void Awake()
    {
        currentCooldown = weaponData.CooldownDuration;
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentPierce = weaponData.Pierce;
        currentKnockBackForce = weaponData.KnockBackForce;
    }
    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        //if collision with enemy then take damage
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            Transform playerTranform = FindObjectOfType<PlayerStats>().transform;
            Vector3 direction = (enemyStats.transform.position - playerTranform.position).normalized;
            enemyStats.GetComponent<Rigidbody2D>().AddForce(direction * currentKnockBackForce, ForceMode2D.Impulse);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //if collision with enemy then take damage
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(GetCurrentDamage());
            Debug.Log("take damage");
        }
        else if (collision.CompareTag("Prop"))
        {
            if (collision.TryGetComponent(out BreakableProps breakableProps))
            {
                breakableProps.TakeDamage(GetCurrentDamage());
            }
        }
    }
}
