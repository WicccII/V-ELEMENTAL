using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidPassiveItem : PassiveItem
{
   
    protected override void ApplyMultiplier()
    {
        player.CurrentMight *= 1 + passiveItemData.Multiplier / 100f;
    }
    protected override void ApplyAddition()
    {
        player.CurrentRecovery += passiveItemData.Addition;
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
