using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    
    public int maxHealth = 100;
    private int currentHealth;

    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 moveDirection;
    public bool facingRight = true;

    protected void Start()
    {
        currentHealth = maxHealth;
        moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        // Movement
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public void Move(float moveX, float moveY)
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Do hurt animation

        if ( currentHealth <= 0)
        {
            Debug.Log("Died");
        }
    }
}
