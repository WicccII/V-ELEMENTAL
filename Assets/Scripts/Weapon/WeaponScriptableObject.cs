using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab {get => prefab; private set => prefab = value;}
    //base stats for weapon
    [SerializeField]
    float damage;
    public float Damage {get => damage; private set => damage = value;}
    [SerializeField]
    float speed;
    public float Speed {get => speed; private set => speed = value;}
    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration {get => cooldownDuration; private set => cooldownDuration = value;}
    [SerializeField]
    float pierce;
    public float Pierce {get => pierce; private set => pierce = value;}
}
