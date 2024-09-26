using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public List<SkillController> skillSlots = new List<SkillController>(6);
    public int[] skillLevels = new int[6];
    public List<Sprite> skillUI = new List<Sprite>(6);
    public List<PassiveItem> itemSlots = new List<PassiveItem>(6);
    public int[] itemLevels = new int[6];
    public List<Sprite> itemUI = new List<Sprite>(6);

    [System.Serializable]
    public class SkillUpgrade
    {
        public int skillUpgradeIndex;
        public GameObject initialSkill;
        public SkillScriptableObject skillData;
    }

    [System.Serializable]
    public class ItemUpgrade
    {
        public int itemUpgradeIndex;
        public GameObject initialItem;
        public PassiveItemScriptableObject itemData;
    }
    [Header("Upgrade Options")]
    public List<SkillUpgrade> skillUpgradesOption = new List<SkillUpgrade>();
    public List<ItemUpgrade> itemUpgradesOption = new List<ItemUpgrade>();

    void Awake()
    {
    }

    public void AddSkill(int indexSlot, SkillController skill)
    {
        skillSlots[indexSlot] = skill;
        skillLevels[indexSlot] = skill.skillData.Level;
        skillUI[indexSlot] = skill.skillData.Icon;

        if (GameManager.Instance != null && GameManager.Instance.chooseUpgrade)
        {
            GameManager.Instance.EndLevelUp();
        }
    }

    public void AddItem(int indexSlot, PassiveItem item)
    {
        itemSlots[indexSlot] = item;
        itemLevels[indexSlot] = item.passiveItemData.Level;
        itemUI[indexSlot] = item.passiveItemData.Icon;

        if (GameManager.Instance != null && GameManager.Instance.chooseUpgrade)
        {
            GameManager.Instance.EndLevelUp();
        }
    }

    public void LevelUpSkill(int indexSlot, int skillUpgradeIndex)
    {
        if (skillSlots.Count > indexSlot)
        {
            SkillController skill = skillSlots[indexSlot];
            if (!skill.skillData.NextLevelPrefab)
            {
                Debug.LogError("No next level prefab : " + skill.name);
                return;
            }
            GameObject upgrdeSkill = Instantiate(skill.skillData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgrdeSkill.transform.SetParent(transform);
            AddSkill(indexSlot, upgrdeSkill.GetComponent<SkillController>());
            Destroy(skill.gameObject);
            skillUpgradesOption[skillUpgradeIndex].skillData = upgrdeSkill.GetComponent<SkillController>().skillData;
            skillLevels[indexSlot] = upgrdeSkill.GetComponent<SkillController>().skillData.Level;
        }
    }

    public void LevelUpItem(int indexSlot, int itemUpgradeIndex)
    {
        if (itemSlots.Count > indexSlot)
        {
            PassiveItem item = itemSlots[indexSlot];
            GameObject upgrdeItem = Instantiate(item.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgrdeItem.transform.SetParent(transform);
            AddItem(indexSlot, upgrdeItem.GetComponent<PassiveItem>());
            Destroy(item.gameObject);
            itemUpgradesOption[itemUpgradeIndex].itemData = upgrdeItem.GetComponent<PassiveItem>().passiveItemData;
            skillLevels[indexSlot] = upgrdeItem.GetComponent<PassiveItem>().passiveItemData.Level;
        }
    }
}
