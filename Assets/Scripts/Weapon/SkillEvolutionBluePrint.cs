using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/SkillEvolutionBluePrint")]
public class SkillEvolutionBluePrint : ScriptableObject
{
    public SkillScriptableObject baseSkillDate;
    public PassiveItemScriptableObject catalystPassiveItemData;
    public SkillScriptableObject evoluteSkillData;
    public GameObject evoluteSkill;
}
