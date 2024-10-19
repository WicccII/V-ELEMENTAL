using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossController : MeeleSkillBehaviour
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
        audioManager.PlaySFX(audioManager.crossAttack);
        base.Attack();
        GetComponent<MeeleSkillBehaviour>().DirectionChecker(playerMove.lastMoveDirection);
    }
}
