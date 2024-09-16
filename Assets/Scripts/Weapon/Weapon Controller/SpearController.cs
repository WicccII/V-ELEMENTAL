using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Attack()
    {
        base.Attack();
        GameObject spawmedSpear = Instantiate(weaponData.Prefab);
        spawmedSpear.transform.position = transform.position;//waepon spawn direct where player position 
        spawmedSpear.GetComponent<SpearBehaviour>().DirectionChecker(playerMove.lastMoveDirection);//set direction where weapon gonna go
    }
}
