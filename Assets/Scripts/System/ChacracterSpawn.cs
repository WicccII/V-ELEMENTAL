using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChacracterSpawn : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        player = CharacterSelecter.GetCharacter().Character;
        Instantiate(player, transform.position, Quaternion.identity);
        CharacterSelecter.instance.DestroySingleTon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
