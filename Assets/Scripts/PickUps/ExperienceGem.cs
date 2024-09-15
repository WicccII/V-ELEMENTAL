using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : PickUps, ICollectable
{
    public int expGrandeted;
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(expGrandeted);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}