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

    void OnDestroy()
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
