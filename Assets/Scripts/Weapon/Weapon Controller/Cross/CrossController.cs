using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossController : MeeleSkillBehaviour
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        base.Attack();
        GetComponent<MeeleSkillBehaviour>().DirectionChecker(playerMove.lastMoveDirection);
    }
}
