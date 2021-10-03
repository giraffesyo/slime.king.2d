using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D rb;

    private Vector2 moveDirection;
    private bool facingRight = true;


    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        // Movement
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    protected void Move(float moveX, float moveY)
    {
        if ((moveX < 0 && facingRight) || (moveX > 0 && !facingRight))
        {
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    public void setSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
