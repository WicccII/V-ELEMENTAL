using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelecter : MonoBehaviour
{
    public static CharacterSelecter instance;
    public PlayerScriptableObject playerScriptableObject;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("EXTRA"+ this + "DESTROY");
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static PlayerScriptableObject GetCharacter()
    {
        return instance.playerScriptableObject;
    }

    public void SetCharacter(PlayerScriptableObject character)
    {
        playerScriptableObject = character;
    }

    public void DestroySingleTon()
    {
        instance = null;
        Destroy(gameObject);
    }
}
