using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

//base weapon stat of all weapon
public class WeaponController : MonoBehaviour
{
    [Header("Weapon stats")]
    public WeaponScriptableObject weaponData;
    float currentCooldown;

    protected PlayerMove playerMove;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentCooldown = weaponData.CooldownDuration;
        playerMove = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.CooldownDuration;
    }
}
