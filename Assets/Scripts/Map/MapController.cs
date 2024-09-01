using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerraPos;
    public LayerMask terrainMask;
    PlayerMove playerMove;
    public GameObject currentChunk;

    [Header("Optimize")]
    public List<GameObject> spawnChunks;
    GameObject lastChunk;
    public float maxOpDistance;
    float opDistance;
    float opCoolDown;
    public float opCoolDownTime;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        ChunkOptimize();
    }

    void ChunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }
        if (playerMove.moveDirection.x > 0 || playerMove.moveDirection.y == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerraPos = currentChunk.transform.Find("Right").position;  //Right
                SpawnChunk();
            }
        }
        if (playerMove.moveDirection.x < 0 || playerMove.moveDirection.y == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerraPos = currentChunk.transform.Find("Left").position;    //Left
                SpawnChunk();
            }
        }
        if (playerMove.moveDirection.y > 0 || playerMove.moveDirection.x == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerraPos = currentChunk.transform.Find("Up").position; //Up
                SpawnChunk();
            }
        }
        if (playerMove.moveDirection.y < 0 || playerMove.moveDirection.x == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerraPos = currentChunk.transform.Find("Down").position;    //Down
                SpawnChunk();
            }
        }
        if (playerMove.moveDirection.x > 0 || playerMove.moveDirection.y > 0 )
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("RightUp").position, checkerRadius, terrainMask))
            {
                noTerraPos = currentChunk.transform.Find("RightUp").position;   //Right up
                SpawnChunk();
            }
        }
        if (playerMove.moveDirection.x > 0 || playerMove.moveDirection.y < 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("RightDown").position, checkerRadius, terrainMask))
            {
                noTerraPos = currentChunk.transform.Find("RightDown").position;  //Right down
                SpawnChunk();
            }
        }
        if (playerMove.moveDirection.x < 0 || playerMove.moveDirection.y > 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("LeftUp").position, checkerRadius, terrainMask))
            {
                noTerraPos = currentChunk.transform.Find("LeftUp").position;  //Left up
                SpawnChunk();
            }
        }
        if (playerMove.moveDirection.x < 0 || playerMove.moveDirection.y < 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("LeftDown").position, checkerRadius, terrainMask))
            {
                noTerraPos = currentChunk.transform.Find("LeftDown").position; //Left down
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        lastChunk = Instantiate(terrainChunks[rand], noTerraPos, Quaternion.identity);
        spawnChunks.Add(lastChunk);
    }

    void ChunkOptimize()
    {
        opCoolDown -= Time.deltaTime;
        if (opCoolDown <= 0f)
        {
            opCoolDown = opCoolDownTime;
        }else
        {
            return;
        }
    
        foreach (GameObject chunk in spawnChunks)
        {
            opDistance = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opDistance > maxOpDistance)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
