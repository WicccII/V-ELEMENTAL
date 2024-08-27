using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D myRigidbody2D;
    Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
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
    }
    private void Move()
    {
        myRigidbody2D.velocity = new Vector2(moveDirection.x * moveSpeed * Time.deltaTime, moveDirection.y * moveSpeed * Time.deltaTime);
    }
}
