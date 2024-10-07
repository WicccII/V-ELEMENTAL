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

    [Header("UI")]
    public Image expbar;
    public List<Image> SkillUIIcon = new List<Image>(6);
    public List<Image> itemUIcon = new List<Image>(6);

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
        AddSkill();
        AddItemUI();
    }

    void Update()
    {
        AddSkill();
        AddItemUI();
        UpdateEXPbar();
    }

    void UpdateEXPbar()
    {
        expbar.fillAmount = (float)playerStatsSript.experience / playerStatsSript.expierienCap;
    }

    public void AddSkill()
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

    public void AddItemUI()
    {
        if (inventory.itemUI.Count > itemUIcon.Count)
        {
            return;
        }
        for (int i = 0; i < itemUIcon.Count; i++)
        {
            if (inventory.itemUI[i])
            {
                itemUIcon[i].sprite = inventory.itemUI[i];
                itemUIcon[i].enabled = true;
            }
            else
            {
                itemUIcon[i].enabled = false;
            }
        }
    }

    void ApplyUpgradeOption()
    {
        int skillIndex = 0;
        int itemIndex = 0;

        int countAvailableUpgrade = 0;

        List<SkillUpgrade> availableSkillUpgradesOption = new List<SkillUpgrade>(inventory.skillUpgradesOption);
        List<ItemUpgrade> availableItemUpgradesOption = new List<ItemUpgrade>(inventory.itemUpgradesOption);

        for (int i = 0; i < 6; i++)
        {
            if (inventory.skillSlots[i] != null)
            {
                skillIndex++;
            }
            if (inventory.itemSlots[i] != null)
            {
                itemIndex++;
            }
        }

        //check if skill slot is full or not and item is full or not
        if (skillIndex > inventory.skillSlots.Count)
        {
            RemoveUpgradeOptionWhenSkillFull(availableSkillUpgradesOption, skillIndex);
            Debug.Log("skill slot full");
        }
        if (itemIndex > inventory.itemSlots.Count)
        {
            RemoveUpgradeOptionWhenItemFull(availableItemUpgradesOption, itemIndex);
            Debug.Log("item slot full");
        }

        //remove allready max upgrades skill and item
        RemoveAllreadyMaxUgrades(availableSkillUpgradesOption, availableItemUpgradesOption);

        foreach (var upgrade in upgradeUIOption)
        {
            if (countAvailableUpgrade >= 3)
            {
                GameManager.Instance.EndLevelUp();
                return;
            }

            //Debug.Log("Skill: " + availableSkillUpgradesOption.Count + " Item: " + availableItemUpgradesOption.Count);
            if (availableSkillUpgradesOption.Count == 0 && availableItemUpgradesOption.Count == 0)
            {
                countAvailableUpgrade++;
                return;
            }

            int upgradeType;
            if (availableSkillUpgradesOption.Count == 0)
            {
                upgradeType = 2;
            }
            else if (availableItemUpgradesOption.Count == 0)
            {
                upgradeType = 1;
            }
            else
            {
                upgradeType = Random.Range(1, 3);
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
                        if (inventory.skillSlots[i] != null && inventory.skillSlots[i].skillData == chosenSkillUpgrade.skillData)
                        {
                            newSkill = false;
                            if (!newSkill)
                            {
                                if (!chosenSkillUpgrade.skillData.NextLevelPrefab)
                                {
                                    countAvailableUpgrade++;
                                    disableUpgradeUI(upgrade);
                                    break;
                                }
                                upgrade.upgradeButton.onClick.AddListener(() => inventory.LevelUpSkill(i, chosenSkillUpgrade.skillUpgradeIndex));
                                upgrade.upgradeIcon.sprite = chosenSkillUpgrade.skillData.NextLevelPrefab.GetComponent<SkillController>().skillData.Icon;
                                upgrade.upgradeName.text = chosenSkillUpgrade.skillData.NextLevelPrefab.GetComponent<SkillController>().skillData.Name;
                                upgrade.ugradeDescription.text = chosenSkillUpgrade.skillData.NextLevelPrefab.GetComponent<SkillController>().skillData.Description;
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
            else if (upgradeType == 2)
            {
                ItemUpgrade chosenItemUpgrade = availableItemUpgradesOption[Random.Range(0, availableItemUpgradesOption.Count)];
                availableItemUpgradesOption.Remove(chosenItemUpgrade);
                if (chosenItemUpgrade != null)
                {
                    enableUpgradeUI(upgrade);
                    bool newItem = false;
                    for (int i = 0; i < inventory.itemSlots.Count; i++)
                    {
                        if (inventory.itemSlots[i] != null && inventory.itemSlots[i].passiveItemData == chosenItemUpgrade.itemData)
                        {
                            newItem = false;
                            if (!newItem)
                            {
                                if (!chosenItemUpgrade.itemData.NextLevelPrefab)
                                {
                                    countAvailableUpgrade++;
                                    disableUpgradeUI(upgrade);
                                    break;
                                }
                                upgrade.upgradeButton.onClick.AddListener(() => inventory.LevelUpItem(i, chosenItemUpgrade.itemUpgradeIndex));
                                upgrade.upgradeIcon.sprite = chosenItemUpgrade.itemData.NextLevelPrefab.GetComponent<PassiveItem>().passiveItemData.Icon;
                                upgrade.upgradeName.text = chosenItemUpgrade.itemData.NextLevelPrefab.GetComponent<PassiveItem>().passiveItemData.Name;
                                upgrade.ugradeDescription.text = chosenItemUpgrade.itemData.NextLevelPrefab.GetComponent<PassiveItem>().passiveItemData.Description;
                            }
                            break;
                        }
                        else
                        {
                            newItem = true;
                        }
                    }
                    if (newItem)
                    {
                        upgrade.upgradeButton.onClick.AddListener(() => player.GetComponent<PlayerStats>().SpawnItem(chosenItemUpgrade.initialItem));
                        upgrade.upgradeIcon.sprite = chosenItemUpgrade.itemData.Icon;
                        upgrade.upgradeName.text = chosenItemUpgrade.itemData.Name;
                        upgrade.ugradeDescription.text = chosenItemUpgrade.itemData.Description;
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

    void RemoveAllreadyMaxUgrades(List<SkillUpgrade> availableSkillUpgradesOption, List<ItemUpgrade> availableItemUpgradesOption)
    {
        List<SkillUpgrade> skillUpgradesToRemove = new List<SkillUpgrade>();

        for (int i = 0; i < availableSkillUpgradesOption.Count; i++)
        {
            if (inventory.skillSlots != null)
            {
                for (int j = 0; j < inventory.skillSlots.Count; j++)
                {
                    if (inventory.skillSlots[j] != null &&
                        availableSkillUpgradesOption[i].skillData.Prefab == inventory.skillSlots[j].skillData.Prefab && availableSkillUpgradesOption[i].skillData.NextLevelPrefab == null)
                    {
                        skillUpgradesToRemove.Add(availableSkillUpgradesOption[i]);
                    }
                }
            }
        }

        // Xóa tất cả sau khi vòng lặp hoàn tất
        foreach (var skillUpgrade in skillUpgradesToRemove)
        {
            availableSkillUpgradesOption.Remove(skillUpgrade);
        }
        List<ItemUpgrade> itemUpgradesToRemove = new List<ItemUpgrade>();

        for (int i = 0; i < availableItemUpgradesOption.Count; i++)
        {
            if (inventory.itemSlots != null)
            {
                for (int j = 0; j < inventory.itemSlots.Count; j++)
                {
                    if (inventory.itemSlots[j] != null &&
                        availableItemUpgradesOption[i].itemData.Prefab == inventory.itemSlots[j].passiveItemData.Prefab && availableItemUpgradesOption[i].itemData.NextLevelPrefab == null)
                    {
                        itemUpgradesToRemove.Add(availableItemUpgradesOption[i]);
                    }
                }
            }
        }

        // Xóa tất cả sau khi vòng lặp hoàn tất
        foreach (var itemUpgrade in itemUpgradesToRemove)
        {
            availableItemUpgradesOption.Remove(itemUpgrade);
        }
    }

    void RemoveUpgradeOptionWhenSkillFull(List<SkillUpgrade> avaiSkill, int skillIndex)
    {
        List<SkillUpgrade> skillUpgradesToRemove = new List<SkillUpgrade>();

        for (int i = 0; i < avaiSkill.Count; i++)
        {
            int duplicateCount = 0;

            if (inventory.skillSlots != null)
            {
                for (int j = 0; j < inventory.skillSlots.Count; j++)
                {
                    if (inventory.skillSlots[j] != null && inventory.skillSlots[j].skillData != avaiSkill[i].skillData)
                    {
                        duplicateCount++; // increase duplicateCount
                    }
                }
            }

            // If the number of duplicates exceeds the skillIndex, add the skillUpgrade to the list to be removed
            if (duplicateCount >= skillIndex)
            {
                skillUpgradesToRemove.Add(avaiSkill[i]);
            }
        }

        // Xóa các kỹ năng sau khi vòng lặp hoàn tất
        foreach (var skillUpgrade in skillUpgradesToRemove)
        {
            avaiSkill.Remove(skillUpgrade);
        }
    }

    void RemoveUpgradeOptionWhenItemFull(List<ItemUpgrade> avaiItem, int itemIndex)
    {
        List<ItemUpgrade> itemUpgradesToRemove = new List<ItemUpgrade>();

        for (int i = 0; i < avaiItem.Count; i++)
        {
            int duplicateCount = 0;
            if (inventory.itemSlots != null)
            {
                for (int j = 0; j < inventory.itemSlots.Count; j++)
                {
                    if (inventory.itemSlots[j] != null && inventory.itemSlots[j].passiveItemData != avaiItem[i].itemData)
                    {
                        duplicateCount++; // increase duplicateCount
                    }
                }
            }

            // If the number of duplicates exceeds the skillIndex, add the skillUpgrade to the list to be removed
            if (duplicateCount >= itemIndex)
            {
                itemUpgradesToRemove.Add(avaiItem[i]);
            }
        }

        foreach (var itemUpgrade in itemUpgradesToRemove)
        {
            avaiItem.Remove(itemUpgrade);
        }
    }
}