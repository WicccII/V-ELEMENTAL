using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItem", menuName = "ScriptableObject/PassiveItem")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }
    [SerializeField]
    float multiplier;
    public float Multiplier { get => multiplier; set => multiplier = value; }
    [SerializeField]
    float addition;
    public float Addition { get => addition; set => addition = value; }
    [SerializeField]
    int level; //Modifile in editor only 
    public int Level { get => level; private set => level = value; }
    [SerializeField]
    GameObject nextLevelPrefab; //the prefab of next level which object become when level up
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }
    [SerializeField]
    Sprite icon;//Modifile in editor only 
    public Sprite Icon { get => icon; private set => icon = value; }
    [SerializeField]
    new string name;//Modifile in editor only 
    public string Name { get => name; private set => Name = name; }
    [SerializeField]
    string description;//Modifile in editor only 
    public string Description { get => description; private set => Description = description; }
}
