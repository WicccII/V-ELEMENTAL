using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> skillSlots = new List<WeaponController>(6);
    public int[] skillLevels = new int[6];
    public List<Sprite> skillUI = new List<Sprite>(6);
    public List<PassiveItem> itemSlots = new List<PassiveItem>(6);
    public int[] itemLevels = new int[6];

    [System.Serializable]
    public class SkillUpgrade
    {
        public int skillUpgradeIndex;
        public GameObject initialSkill;
        public WeaponScriptableObject skillData;
    }
    public List<SkillUpgrade> skillUpgradesOption = new List<SkillUpgrade>();

    void Awake ()
    {
    }

    public void AddSkill(int indexSlot, WeaponController skill)
    {
        skillSlots[indexSlot] = skill;
        skillLevels[indexSlot] = skill.weaponData.Level;
        skillUI[indexSlot] = skill.weaponData.Icon;

        if (GameManager.Instance != null && GameManager.Instance.chooseUpgrade)
        {
            GameManager.Instance.EndLevelUp();
        }
    }

    public void AddItem(int indexSlot, PassiveItem item)
    {
        itemSlots[indexSlot] = item;
        itemLevels[indexSlot] = item.passiveItemData.Level;
    }

    public void LevelUpSkill(int indexSlot, int skillUpgradeIndex)
    {
        if (skillSlots.Count > indexSlot)
        {
            WeaponController skill = skillSlots[indexSlot];
            if (!skill.weaponData.NextLevelPrefab)
            {
                Debug.LogError("No next level prefab : " + skill.name);
                return;
            }
            GameObject upgrdeSkill = Instantiate(skill.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgrdeSkill.transform.SetParent(transform);
            AddSkill(indexSlot, upgrdeSkill.GetComponent<WeaponController>());
            Destroy(skill.gameObject);
            skillUpgradesOption[skillUpgradeIndex].skillData = upgrdeSkill.GetComponent<WeaponController>().weaponData;
            skillLevels[indexSlot] = upgrdeSkill.GetComponent<WeaponController>().weaponData.Level;
        }
    }

    public void LevelUpItem(int indexSlot)
    {
        if (itemSlots.Count > indexSlot)
        {
            PassiveItem item = itemSlots[indexSlot];
            GameObject upgrdeItem = Instantiate(item.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgrdeItem.transform.SetParent(transform);
            AddItem(indexSlot, upgrdeItem.GetComponent<PassiveItem>());
            Destroy(item.gameObject);
            skillLevels[indexSlot] = upgrdeItem.GetComponent<PassiveItem>().passiveItemData.Level;
        }
    }
}
