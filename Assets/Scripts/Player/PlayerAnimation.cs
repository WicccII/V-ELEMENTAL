using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    public PlayerMove playerMove;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMove.moveDirection.x != 0 || playerMove.moveDirection.y != 0)
        {
            animator.SetBool("Move", true);
            SpriteDirection();
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }

    public void SpriteDirection()
    {
        if (playerMove.moveDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (playerMove.moveDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
