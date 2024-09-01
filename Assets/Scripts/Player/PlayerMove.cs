using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D myRigidbody2D;
    [HideInInspector]
    public Vector2 moveDirection;
    [HideInInspector]
    public Vector2 lastMoveDirection;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        lastMoveDirection = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        InputManagaer();
    }


    private void FixedUpdate()
    {
        Move();
    }

    void InputManagaer()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(x, y).normalized;

        //last move Input
        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            lastMoveDirection = new Vector2(moveDirection.x, moveDirection.y);
        }
    }
    private void Move()
    {
        myRigidbody2D.velocity = new Vector2(moveDirection.x * moveSpeed , moveDirection.y * moveSpeed);
    }
}
