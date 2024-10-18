using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlewBlastController : SkillController
{
    AudioManager audioManager;
    // Start is called before the first frame update
    protected override void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        base.Start();
    }

    // Update is called once per frame
    protected override void Attack()
    {
        audioManager.PlaySFX(audioManager.blewBlastCountdown);
        base.Attack();
        GameObject spawnedGalic = Instantiate(skillData.Prefab);
        spawnedGalic.transform.position = transform.position;//spawn direct player póition
        
    }
}
