using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/Character")]
public class PlayerScriptableObject : ScriptableObject
{
    [SerializeField]
    Sprite icon;
    public Sprite Icon { get => icon; set => icon = value; }
    [SerializeField]
    string characterName;
    public string CharacterName { get => characterName; set => characterName = value; }
    [SerializeField]
    GameObject character;
    public GameObject Character {get => character; private set => character = value;}
    [SerializeField]
    GameObject startingWeapon;
    public GameObject StartingWeapon {get => startingWeapon; private set => startingWeapon = value;}
    [SerializeField]
    float maxHealth;
    public float MaxHealth {get => maxHealth; private set => maxHealth = value;}
    [SerializeField]
    float recovery;
    public float Recovery {get => recovery; private set => recovery = value;}
    [SerializeField]
    float moveSpeed;
    public float MoveSpeed {get => moveSpeed; private set => moveSpeed = value;}
    [SerializeField]
    float might;
    public float Might {get => might; private set => might = value;}
    [SerializeField]
    float projectileSpeed;
    public float ProjectileSpeed {get => projectileSpeed; private set => projectileSpeed = value;}
    [SerializeField]
    float magnet;
    public float Magnet {get => magnet; private set => magnet = value;}
}
