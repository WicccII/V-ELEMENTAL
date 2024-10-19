using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    public EnemyScriptableObject enemydata;
    PlayerStats player;
    Vector3 lastdirec;
    Rigidbody2D rb;
    float currentDamage;
    [Header("Arrow Properties")]
    public float speed;
    public float destroyAfter;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyAfter);
        currentDamage = enemydata.Damage;
        player = FindObjectOfType<PlayerStats>();
        lastdirec = player.transform.position;

        rb = GetComponent<Rigidbody2D>();

        // Tính toán hướng từ vị trí hiện tại đến lastdirec
        Vector2 direction = (lastdirec - transform.position).normalized;

        // Xoay mũi tên theo hướng bay
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Áp dụng lực cho Rigidbody2D theo hướng đã tính với tốc độ
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(currentDamage);
            Destroy(gameObject);
        }
    }
}
