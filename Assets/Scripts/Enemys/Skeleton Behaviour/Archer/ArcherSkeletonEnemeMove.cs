using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherSkeletonEnemeMove : MonoBehaviour
{
        Transform player;
    public EnemyScriptableObject enemyData;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
        FlipEnemy();
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.Speed * Time.deltaTime);
    }

    void FlipEnemy()
    {
      
        if (transform.position.x > player.position.x && isFacingRight)
        {
            Flip();
        }
       
        else if (transform.position.x < player.position.x && !isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
 
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
