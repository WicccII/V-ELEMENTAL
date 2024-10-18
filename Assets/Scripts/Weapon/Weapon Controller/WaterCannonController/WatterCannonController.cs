using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatterCannonController : MeeleTracerSkillBehaviour
{
    AudioManager audioManager;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        UpdateDirection();//set direction where weapon gonna go
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        audioManager.PlaySFX(audioManager.waterCanonAttack);
        base.Attack();
        UpdateDirection();//set direction where weapon gonna go
    }
}
