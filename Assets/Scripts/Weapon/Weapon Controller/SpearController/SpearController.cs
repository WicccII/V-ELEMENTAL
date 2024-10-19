using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : SkillController
{
    // Start is called before the first frame update
    AudioManager audioManager;
    protected override void Start()
    {
        base.Start();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    protected override void Attack()
    {   
      
        audioManager.PlaySFX(audioManager.spearAttack);
        base.Attack();
        GameObject spawmedSpear = Instantiate(skillData.Prefab);
        spawmedSpear.transform.position = transform.position;//waepon spawn direct where player position 
        spawmedSpear.GetComponent<SpearBehaviour>().DirectionChecker(playerMove.lastMoveDirection);//set direction where weapon gonna go

    }
}
