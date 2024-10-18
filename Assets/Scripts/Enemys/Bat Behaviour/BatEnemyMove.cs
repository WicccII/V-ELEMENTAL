using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BatEnemyMove : MonoBehaviour
{
    Transform player;
    public EnemyScriptableObject enemyData;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>().transform;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.Speed * Time.deltaTime); 
    }
}
