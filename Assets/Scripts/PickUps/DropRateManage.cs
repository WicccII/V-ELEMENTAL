using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManage : MonoBehaviour
{
    [System.Serializable]
    public class Drop
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }

    public List<Drop> drops;
    private bool isQuitting = false;

    void OnApplicationQuit()
    {
        isQuitting = true;  // This will be set when the application is quitting
    }

    void OnDestroy()
    {
        if (isQuitting) return;  // Don't drop items if the application is quitting
        
        if (!Application.isPlaying || !gameObject.scene.isLoaded)// Check if the game is still playing and the scene is not being unloaded
            return;
        DropItem();  // Call the method to handle item drop
    }

    void DropItem()
    {
        float randomNumber = Random.Range(0f, 100f);
        List<Drop> possibleDrops = new List<Drop>();

        foreach (Drop rate in drops)
        {
            if (randomNumber <= rate.dropRate)
            {
                possibleDrops.Add(rate);
            }
        }

        if (possibleDrops.Count > 0)
        {
            Drop drop = possibleDrops[Random.Range(0, possibleDrops.Count)];
            Instantiate(drop.itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
