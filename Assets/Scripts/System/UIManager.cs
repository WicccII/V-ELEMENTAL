using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public InventoryManager inventory;
    // Start is called before the first frame update
    void Awake()
    {
        inventory = FindObjectOfType<InventoryManager>();
        // After spawning the player, initialize skills and update UI
        AddSkillUI();
    }

    public void AddSkillUI()
    {
        for (int i = 0; i < inventory.skillUI.Count; i++)
        {
            // Construct the name of the UI component
            string skillSlotName = "Skill Slot " + (i + 1);

            // Find the GameObject with the specified name and get the Image component
            Image skillSlotImage = GameObject.Find(skillSlotName)?.GetComponent<Image>();

            if (skillSlotImage != null)
            {
                // Add the Image component to the inventory list
                inventory.skillUI[i] = skillSlotImage;

                // Update the sprite if weapon data is available
                if (i < inventory.skillSlots.Count && inventory.skillSlots[i]?.weaponData != null)
                {
                    inventory.skillUI[i].sprite = inventory.skillSlots[i].weaponData.Icon;
                    inventory.skillUI[i].enabled = true;
                }
                else
                {
                    inventory.skillUI[i].enabled = false;
                }
            }
        }
    }

    public void LevelUpSkill(int skillIndex)
    {
        inventory.LevelUpSkill(skillIndex);
    }

}
