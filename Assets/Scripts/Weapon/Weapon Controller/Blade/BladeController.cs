using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BladeController : MeeleSkillBehaviour
{
    AudioManager audioManager;
 

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        audioManager.PlaySFX(audioManager.bladeAttack);
        base.Attack();
        GetComponent<MeeleSkillBehaviour>().DirectionChecker(playerMove.lastMoveDirection);
    }
}
