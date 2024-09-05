using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base script of all meele weapon
public class MeeleWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    public float destroyTime;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
