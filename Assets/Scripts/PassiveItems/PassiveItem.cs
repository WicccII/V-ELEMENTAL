using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats player;
    public PassiveItemScriptableObject passiveItemData;

    protected virtual void ApplyMultiplier()
    {
        //Aplly the boost to the player
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerStats>();

        ApplyMultiplier();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
