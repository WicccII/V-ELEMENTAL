using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : PickUps, ICollectable
{
    public int hpGrandted;
    public void Collect()
    {
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
