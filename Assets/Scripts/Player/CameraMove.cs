using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform taget;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        taget = FindObjectOfType<PlayerStats>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = taget.position + offset;
    }
}
