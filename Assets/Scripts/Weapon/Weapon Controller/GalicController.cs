using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GalicController : SkillController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        currentCooldown = 0f;
    }

    // Update is called once per frame
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGalic = Instantiate(skillData.Prefab);
        spawnedGalic.transform.position = transform.position;//spawn direct player póition
        spawnedGalic.transform.parent  = transform;//follow player
    }
}
