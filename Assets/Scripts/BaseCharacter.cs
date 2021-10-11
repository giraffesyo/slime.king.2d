using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BaseCharacter : Damageable
{

    public Transform attackPoint;

    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 moveDirection;
    public bool invincible; 
    public bool facingRight = true;
    public bool stunned;
    private SpriteRenderer spriteRenderer;
   

    public float moveX = 0;
    public float moveY = 0;

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetMaxHealth(initialMaxHealth);
        moveSpeed = 5f;
    }
    protected IEnumerator ActivateInvincibility(float forSeconds)
    {
        invincible = true;

        // TODO: animate invincibility
        yield return new WaitForSeconds(forSeconds);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (currentHealth < 1)
        {
            Debug.Log($"{transform.name} died");
            Die();
        }
    }

    protected void FixedUpdate()
    {
        // Movement
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public void Move(float moveX, float moveY)
    {
        if (stunned)
            return;

        if ((moveX < 0 && facingRight) || (moveX > 0 && !facingRight))
        {
            facingRight = !facingRight;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        moveDirection = new Vector2(moveX, moveY).normalized;
        Debug.Log($"moveDirection is  {moveDirection}");
    }

    public void setSpeed(float speed)
    {
        moveSpeed = speed;
    }


    protected virtual void Die()
    {
        // play the dying animation 
        // play dying sound for this unit
    }

    public void moveAttackPoint()
    {
        if (moveX == 0 && moveY == 0)
        {

            attackPoint.localPosition = new Vector3(.65f, 0, 0);
            return;
        }
        attackPoint.localPosition = new Vector3(moveX * .65f, moveY * .65f, 0);
    }

    public IEnumerator Knockback(float knockbackPower, Transform obj)
    { 
        stunned = true;
        Move(0, 0);
        Vector2 direction = (obj.transform.position - this.transform.position).normalized;
        transform.DOMove(new Vector3(transform.position.x - (direction.x * knockbackPower), transform.position.y - (direction.y * knockbackPower),0), 0.5f);
        stunned = false;
        yield return 0;
    }
}
