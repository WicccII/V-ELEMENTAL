using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChacracterSpawn : MonoBehaviour
{
    GameObject player;
    public InventoryManager inventory;
    // Start is called before the first frame update
    void Awake()
    {
        player = CharacterSelecter.GetCharacter().Character;

        Instantiate(player, transform.position, Quaternion.identity);

        CharacterSelecter.instance.DestroySingleTon();

        // After spawning the player, initialize skills and update UI
    }

    // Update is called once per frame
    void Update()
    {

    }
}