using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobingAnimation : MonoBehaviour
{
    public float frequency; //speed of movement
    public float magnitude; //range of movement
    public Vector3 direction; // direction of movement
    Vector3 intitialPosition;
    PickUps pickUps;
    // Start is called before the first frame update
    void Start()
    {
        pickUps = GetComponent<PickUps>();
        intitialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pickUps && !pickUps.hasBeenColletd)
        {
            transform.position = intitialPosition + (direction * Mathf.Sin(Time.time * frequency) * magnitude);
        }
    }
}
