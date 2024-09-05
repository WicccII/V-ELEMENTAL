using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBehaviour : ProjectileWeaponBehaviour
{
    SpearController spearController;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        spearController = FindObjectOfType<SpearController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * spearController.weaponData.speed * Time.deltaTime;
    }
}
