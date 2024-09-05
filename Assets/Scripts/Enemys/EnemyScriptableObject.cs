using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab {get => prefab; private set => prefab = value;}
    //base stats for enemy
    [SerializeField]
    float health;
    public float Health {get => health; private set => health = value;}
    [SerializeField]
    float damage;
    public float Damage {get => damage; private set => damage = value;}
    [SerializeField]
    float speed;
    public float Speed {get => speed; private set => speed = value;}
}
