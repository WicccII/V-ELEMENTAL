using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazingBlashController : SkillController
{
    AudioManager audioManager;
    // Start is called before the first frame update
    protected override void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void Attack()
    {
        audioManager.PlaySFX(audioManager.blazingAttack);
        float randomNumber;
        for (int i = 0; i < skillData.Level; i++)
        {
            base.Attack();
            randomNumber = Random.Range(-0.5f, 0.5f);
            GameObject spawnBlash = Instantiate(skillData.Prefab);
            spawnBlash.transform.position = transform.position + new Vector3(randomNumber, randomNumber, 0);//waepon spawn direct where player position 
            spawnBlash.GetComponent<BlazingBlashBehaviour>().UpdateDirection();//set direction where weapon gonna go
        }
    }
}
