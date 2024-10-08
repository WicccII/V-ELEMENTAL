using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlewBlastController : SkillController
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
        GameObject spawnedGalic = Instantiate(skillData.Prefab);
        spawnedGalic.transform.position = transform.position;//spawn direct player póition
    }
}
