using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingPassive : PassiveItem
{
    protected override void ApplyMultiplier()
    {
        player.currentSpeed *= 1 + passiveItemData.Multiplier / 100f;
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
