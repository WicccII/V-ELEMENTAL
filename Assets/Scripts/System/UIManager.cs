using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static InventoryManager;

public class UIManager : MonoBehaviour
{
    InventoryManager inventory;

    GameObject player;

    public List<Image> SkillUIIcon = new List<Image>(6);

    [System.Serializable]
    public class UpgradeUI
    {
        public Text upgradeName;
        public Text ugradeDescription;
        public Sprite upgradeIcon;
        public Button upgradeButton;
    }

    public List<UpgradeUI> upgradeUIOption = new List<UpgradeUI>();
    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<PlayerStats>().gameObject;

        inventory = FindObjectOfType<InventoryManager>();
        // After spawning the player, initialize skills and update UI
        AddSkillUI();
    }

    void Update()
    {
        AddSkillUI();
    }

    public void AddSkillUI()
    {
        if (inventory.skillUI.Count > SkillUIIcon.Count)
        {
            return;
        }
        for (int i = 0; i < SkillUIIcon.Count; i++)
        {
            if (inventory.skillUI[i])
            {
                SkillUIIcon[i].sprite = inventory.skillUI[i];
                SkillUIIcon[i].enabled = true;
            }
            else
            {
                SkillUIIcon[i].enabled = false;
            }
        }
    }

    public void LevelUpSkill(int skillIndex)
    {
        inventory.LevelUpSkill(skillIndex);
    }

    void ApplyUpgradeOption()
    {
        foreach (var upgrade in upgradeUIOption)
        {
            int upgradeType = Random.Range(1, 1);

            if (upgradeType == 1)
            {
                SkillUpgrade chosenSkillUpgrade = inventory.skillUpgradesOption[Random.Range(0, inventory.skillUpgradesOption.Count)];
                if (chosenSkillUpgrade != null)
                {
                    bool newSkill = false;
                    for (int i = 0; i < inventory.skillSlots.Count; i++)
                    {
                        if (inventory.skillSlots[i] != null && inventory.skillSlots[i].weaponData == chosenSkillUpgrade.skillData)
                        {
                            newSkill = false;
                            if (!newSkill)
                            {
                                upgrade.upgradeButton.onClick.AddListener(() => inventory.LevelUpSkill(i));
                                upgrade.upgradeIcon = chosenSkillUpgrade.skillData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Icon;
                                upgrade.upgradeName.text = chosenSkillUpgrade.skillData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Name;
                                upgrade.ugradeDescription.text = chosenSkillUpgrade.skillData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Description;
                            }
                            break;
                        }
                        else
                        {
                            newSkill = true;
                        }
                    }
                    if (newSkill)
                    {
                        upgrade.upgradeButton.onClick.AddListener(() => player.GetComponent<PlayerStats>().SpawnWeapon(chosenSkillUpgrade.initialSkill));
                    }
                    upgrade.upgradeIcon = chosenSkillUpgrade.skillData.Icon;
                    upgrade.upgradeName.text = chosenSkillUpgrade.skillData.Name;
                    upgrade.ugradeDescription.text = chosenSkillUpgrade.skillData.Description;
                }
            }
        }
    }

    void RemoveUpgradeOption()
    {
        foreach (var upgrade in upgradeUIOption)
        {
            upgrade.upgradeButton.onClick.RemoveAllListeners();
        }
    }

    public void RemoveAndApplyUpgradeOption()
    {
        RemoveUpgradeOption();
        ApplyUpgradeOption();
    }

}
