using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static InventoryManager;

public class UIManager : MonoBehaviour
{
    InventoryManager inventory;

    public GameObject player;

    PlayerStats playerStatsSript;

    public Image expbar;

    public List<Image> SkillUIIcon = new List<Image>(6);

    [System.Serializable]
    public class UpgradeUI
    {
        public Text upgradeName;
        public Text ugradeDescription;
        public Image upgradeIcon;
        public Button upgradeButton;
    }

    public List<UpgradeUI> upgradeUIOption = new List<UpgradeUI>();
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>().gameObject;
        playerStatsSript = FindObjectOfType<PlayerStats>();
        inventory = FindObjectOfType<InventoryManager>();
        // After spawning the player, initialize skills and update UI
        AddSkillUI();
    }

    void Update()
    {
        AddSkillUI();
        UpdateEXPbar();
    }

    void UpdateEXPbar()
    {
        expbar.fillAmount = playerStatsSript.experience / playerStatsSript.levelReach;
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

    void ApplyUpgradeOption()
    {
        List<SkillUpgrade> availableSkillUpgradesOption = new List<SkillUpgrade>(inventory.skillUpgradesOption);
        foreach (var upgrade in upgradeUIOption)
        {
            int upgradeType;
            if (availableSkillUpgradesOption.Count == 0)
            {
                return;
            }
            else
            {
                upgradeType = Random.Range(1, 1);
            }

            if (upgradeType == 1)
            {
                SkillUpgrade chosenSkillUpgrade = availableSkillUpgradesOption[Random.Range(0, availableSkillUpgradesOption.Count)];
                availableSkillUpgradesOption.Remove(chosenSkillUpgrade);
                if (chosenSkillUpgrade != null)
                {
                    enableUpgradeUI(upgrade);
                    bool newSkill = false;
                    for (int i = 0; i < inventory.skillSlots.Count; i++)
                    {
                        if (inventory.skillSlots[i] != null && inventory.skillSlots[i].weaponData == chosenSkillUpgrade.skillData)
                        {
                            newSkill = false;
                            if (!newSkill)
                            {
                                if (!chosenSkillUpgrade.skillData.NextLevelPrefab)
                                {
                                    disableUpgradeUI(upgrade);
                                    break;
                                }
                                upgrade.upgradeButton.onClick.AddListener(() => inventory.LevelUpSkill(i, chosenSkillUpgrade.skillUpgradeIndex));
                                upgrade.upgradeIcon.sprite = chosenSkillUpgrade.skillData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Icon;
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
                        upgrade.upgradeIcon.sprite = chosenSkillUpgrade.skillData.Icon;
                        upgrade.upgradeName.text = chosenSkillUpgrade.skillData.Name;
                        upgrade.ugradeDescription.text = chosenSkillUpgrade.skillData.Description;
                    }
                }
            }
        }
    }

    void RemoveUpgradeOption()
    {
        foreach (var upgrade in upgradeUIOption)
        {
            upgrade.upgradeButton.onClick.RemoveAllListeners();
            disableUpgradeUI(upgrade);
        }
    }

    public void RemoveAndApplyUpgradeOption()
    {
        RemoveUpgradeOption();
        ApplyUpgradeOption();
    }

    void disableUpgradeUI(UpgradeUI upgradeUI)
    {
        upgradeUI.upgradeName.transform.parent.gameObject.SetActive(false);
    }

    void enableUpgradeUI(UpgradeUI upgradeUI)
    {
        upgradeUI.upgradeName.transform.parent.gameObject.SetActive(true);
    }

}
