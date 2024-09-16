using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItem", menuName = "ScriptableObject/PassiveItem")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField]
    float multiplier;
    public float Multiplier { get => multiplier; set => multiplier = value; }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
