using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItem", menuName = "ScriptableObject/PassiveItem")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField]
    float multiplier;
    public float Multiplier { get => multiplier; set => multiplier = value; }
    [SerializeField]
    int level; //Modifile in editor only 
    public int Level { get => level; private set => level = value; }
    [SerializeField]
    GameObject nextLevelPrefab; //the prefab of next level which object become when level up
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }
    [SerializeField]
    Sprite icon;//Modifile in editor only 
    public Sprite Icon {get => icon; private set => icon = value;}
}
