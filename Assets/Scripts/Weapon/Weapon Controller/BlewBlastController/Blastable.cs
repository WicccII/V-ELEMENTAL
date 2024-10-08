using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blastable : MonoBehaviour
{
    bool blast = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Blasting();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            blast = true;
        }
    }

    public bool Blasting()
    {
        return blast;
    }
}
