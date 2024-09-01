using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

//base weapon stat of all weapon
public class WeaponController : MonoBehaviour
{
    [Header("Weapon stats")]
    public GameObject prefab;
    public float damage;
    public float speed;
    public float cooldownDuration;
    float currentCooldown;
    public float pierce;

    protected PlayerMove playerMove;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentCooldown = cooldownDuration;
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
        currentCooldown = cooldownDuration;
    }
}
