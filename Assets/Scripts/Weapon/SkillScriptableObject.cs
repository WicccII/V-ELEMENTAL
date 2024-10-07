using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/Skill")]
public class SkillScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }
    //base stats for weapon
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }
    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }
    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }
    [SerializeField]
    float pierce;
    public float Pierce { get => pierce; private set => pierce = value; }
    [SerializeField]
    float knockBackForce;
    public float KnockBackForce { get => knockBackForce; private set => knockBackForce = value; }
    [SerializeField]
    int level; //Modifile in editor only 
    public int Level { get => level; private set => level = value; }
    [SerializeField]
    GameObject nextLevelPrefab; //the prefab of next level which object become when level up
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }
    [SerializeField]
    Sprite icon;//Modifile in editor only 
    public Sprite Icon { get => icon; private set => icon = value; }
    [SerializeField]
    new string name;//Modifile in editor only 
    public string Name { get => name; private set => Name = name; }
    [SerializeField]
    string description;//Modifile in editor only 
    public string Description { get => description; private set => Description = description; }
    [SerializeField]
    int toRemove;//Modifile in editor only 
    public int ToRemove { get => toRemove; private set => ToRemove = toRemove; }




}
