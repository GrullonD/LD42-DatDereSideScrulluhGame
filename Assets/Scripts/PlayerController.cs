using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] int playerSpeed = 10;
    [SerializeField] int playerJumpPower = 1250;
    [SerializeField] Rigidbody2D rigidBody;

    private bool facingRight = true;
    private float moveX;

    // Use this for initialization
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        // Controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // Animations

        // Player Direction
        if (moveX < 0.0f && facingRight == true)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingRight == false)
        {
            FlipPlayer();
        }

        // Physics
        rigidBody.velocity = new Vector2(moveX * playerSpeed, rigidBody.velocity.y);
    }

    private void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
    }

    private void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;

        localScale.x *= -1;
        transform.localScale = localScale;
    }

}
