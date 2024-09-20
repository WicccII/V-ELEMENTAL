using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

using UIImage = UnityEngine.UI.Image;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> skillSlots = new List<WeaponController>(6);
    public int[] skillLevels = new int[6];
    public List<UIImage> skillUI = new List<UIImage>(6);
    public List<PassiveItem> itemSlots = new List<PassiveItem>(6);
    public int[] itemLevels = new int[6];

    void Awake ()
    {

    }

    public void AddSkill(int indexSlot, WeaponController skill)
    {
        skillSlots[indexSlot] = skill;
        skillLevels[indexSlot] = skill.weaponData.Level;
        skillUI[indexSlot].enabled = true;
        skillUI[indexSlot].sprite = skill.weaponData.Icon;
    }

    public void AddItem(int indexSlot, PassiveItem item)
    {
        itemSlots[indexSlot] = item;
        itemLevels[indexSlot] = item.passiveItemData.Level;
    }

    public void LevelUpSkill(int indexSlot)
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
