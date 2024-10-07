using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : PickUps
{
    public int hpGrandted;
    public override void Collect()
    {
        if (hasBeenColletd)
        {
            return;
        }
        else
        {
            base.Collect();
        }
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        playerStats.IncreaseHealth(hpGrandted);
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
