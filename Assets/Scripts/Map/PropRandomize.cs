using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomize : MonoBehaviour
{
    public List<GameObject> proSawnPoints;
    public List<GameObject> proPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        Randomize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Randomize()
    {
        foreach (GameObject sp in proSawnPoints)
        {
            int rand = Random.Range(0, proPrefabs.Count);
            GameObject go = Instantiate(proPrefabs[rand], sp.transform.position, Quaternion.identity);
            go.transform.parent = sp.transform;
        }
    }
}
