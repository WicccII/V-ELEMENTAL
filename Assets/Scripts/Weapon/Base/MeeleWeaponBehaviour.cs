using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base script of all meele weapon
public class MeeleWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    public float destroyTime;
    //current stats
    float currentCooldown;
    protected float currentDamage;
    float currentSpeed;
    float currentPierce;

    void Awake()
    {
        currentCooldown = weaponData.CooldownDuration;
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentPierce = weaponData.Pierce;
    }
    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().currentMight;
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //if collision with enemy then take damage
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(GetCurrentDamage());
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
