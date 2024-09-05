using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "NewWeapon/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public GameObject prefab;
    //base stats for weapon
    public float damage;
    public float speed;
    public float cooldownDuration;
    public float pierce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
