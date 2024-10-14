using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalicBehaviour : MeeleWeaponBehaviour
{
    public float rotationSpeed = 90f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
