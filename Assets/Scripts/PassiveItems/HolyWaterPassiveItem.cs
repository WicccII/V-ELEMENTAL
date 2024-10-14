using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWaterPassiveItem : PassiveItem
{

    protected override void ApplyMultiplier()
    {
        player.CurrentRecovery *= 1 + passiveItemData.Multiplier / 100f;
    }
    protected override void ApplyAddition()
    {
        player.CurrentHealth += passiveItemData.Addition;
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
