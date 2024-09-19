using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats playerStats;
    CircleCollider2D playerCollider;
    public float pullSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerCollider.radius = playerStats.currentMagnet;
    }

    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out ICollectable collectable))
        {
            //anim pull
            //get item rigidboody
            Rigidbody2D rigidbody2D = collider2D.GetComponent<Rigidbody2D>();
            //direct player and item
            Vector2 forceDirection = (transform.position - collider2D.transform.position).normalized;
            rigidbody2D.AddForce(forceDirection * pullSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out ICollectable collectable))
        {
                collectable.Collect();
        }
    }
}
