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


    [Header("Evolution Skill")]
    public List<SkillEvolutionBluePrint> skillEvolutions = new List<SkillEvolutionBluePrint>();
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

    public List<SkillEvolutionBluePrint> GetPossibleSkillEvolution()
    {
        List<SkillEvolutionBluePrint> possibleSkillEvolution = new List<SkillEvolutionBluePrint>();
        foreach (var skill in skillSlots)
        {
            if (skill != null)
            {
                foreach (var item in itemSlots)
                {
                    if (item != null)
                    {
                        foreach (var evolution in skillEvolutions)
                        {
                            if (skill.skillData.Level >= evolution.baseSkillDate.Level && item.passiveItemData.Level >= evolution.catalystPassiveItemData.Level)
                            {
                                possibleSkillEvolution.Add(evolution);
                            }
                        }
                    }
                }
            }
        }
        return possibleSkillEvolution;
    }

    public void EvoluteSkill(SkillEvolutionBluePrint evolutionBluePrint)
    {
        for (int skillSlot = 0; skillSlot < skillSlots.Count; skillSlot++)
        {
            SkillController skill = skillSlots[skillSlot];
            if(!skill)
            {
                continue;
            }
            for (int itemSlot = 0; itemSlot < itemSlots.Count; itemSlot++)
            {
                PassiveItem item = itemSlots[itemSlot];
                if(!item)
                {
                    continue;
                }
                if (skill && item && skill.skillData.Level >= evolutionBluePrint.baseSkillDate.Level && item.passiveItemData.Level >= evolutionBluePrint.catalystPassiveItemData.Level)
                {
                    GameObject evolutionSkill = Instantiate(evolutionBluePrint.evoluteSkill, transform.position, Quaternion.identity);
                    SkillController evolutionSkillController = evolutionSkill.GetComponent<SkillController>();

                    evolutionSkill.transform.SetParent(transform);
                    AddSkill(skillSlot, evolutionSkillController);
                    Destroy(skill.gameObject);

                    skillLevels[skillSlot] = evolutionSkillController.skillData.Level;
                    skillUI[skillSlot] = evolutionSkillController.skillData.Icon;
                    skillUpgradesOption.RemoveAt(evolutionSkillController.skillData.ToRemove);

                    Debug.Log("Evolution Done");

                    return;
                }
            }
        }
    }
}
