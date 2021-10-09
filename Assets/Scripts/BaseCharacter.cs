using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;

    public Transform attackPoint;

    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 moveDirection;
    public bool facingRight = true;


    public float moveX = 0;
    public float moveY = 0;

    protected void Start()
    {
        currentHealth = maxHealth;
        moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected void FixedUpdate()
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
            Die();
        }
    }
    protected virtual void Die() {
        Debug.Log( $"{transform.name} died");
        // play the dying animation 
        // play dying sound for this unit
        // then finally, delete the unit
        Destroy(this.gameObject);
    }

    public void moveAttackPoint()
    {
        if (moveX == 0 && moveY == 0)
        {
            attackPoint.localPosition = new Vector3(1, 0, 0);
            return;
        }
        // Absoulte value used since when player turns left and right , the whole object gets rotated
        // meaning we dont need to worry about placing attackPoint behind the object
        attackPoint.localPosition = new Vector3(Mathf.Abs(moveX), moveY, 0);
    }
}
