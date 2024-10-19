using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats player;
    protected PlayerScriptableObject playerData;
    public PassiveItemScriptableObject passiveItemData;

    protected virtual void ApplyMultiplier()
    {
        //Aplly the boost to the player
    }
    protected virtual void ApplyAddition()
    {
        //Aplly the boost to the player
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerStats>();

        ApplyMultiplier();

        ApplyAddition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
