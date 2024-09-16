using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    MapController mapController;
    public GameObject tagetMap;
    // Start is called before the first frame update
    void Start()
    {
        mapController = FindObjectOfType<MapController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            mapController.currentChunk = tagetMap;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (mapController.currentChunk == tagetMap)
            {
                mapController.currentChunk = null;
            }
        }
    }
}
