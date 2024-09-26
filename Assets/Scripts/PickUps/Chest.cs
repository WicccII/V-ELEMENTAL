using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    InventoryManager inventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OpenChest();
            Destroy(gameObject);
        }
    }

    public void OpenChest()
    {
        if (inventoryManager.GetPossibleSkillEvolution().Count < 0)
        {
            Debug.Log("no available evolve skill !!!");
        }
        SkillEvolutionBluePrint skillEvolutionBluePrint = inventoryManager.GetPossibleSkillEvolution()[Random.Range(0, inventoryManager.GetPossibleSkillEvolution().Count)];
        inventoryManager.EvoluteSkill(skillEvolutionBluePrint);
    }
}
