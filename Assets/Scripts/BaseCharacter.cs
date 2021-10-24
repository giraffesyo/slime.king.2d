using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : Damageable
{

    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 moveDirection;
    public bool invincible;
    public bool facingRight
    {
        get
        {
            return !spriteRenderer.flipX;
        }
        set
        {
            spriteRenderer.flipX = !value;
        }
    }
    public bool stunned;
    public bool attacking = false;
    private SpriteRenderer spriteRenderer;


    public float moveX = 0;
    public float moveY = 0;

    protected virtual void Start()
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

    protected virtual void FixedUpdate()
    {
        // Movement
        rb.AddForce(moveDirection * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    public void Move(Vector2 direction)
    {
        /*if (stunned)  // Messes with charge ability
        {
            moveDirection = Vector2.zero;
            return;
        }*/
        if (!attacking)
        {
            if (direction.x > 0)
                facingRight = true;
            else if (direction.x < 0)
                facingRight = false;
            //facingRight = direction.x > 0 ? true : false; // Changed it to if else bc if direction.x = 0 it will automatically turn right when standing still (dont know how to do = 0 in this format lol)
            spriteRenderer.flipY = false;
            transform.rotation = Quaternion.identity;
        }
        // if (((direction.x < 0 && facingRight) || (direction.x > 0 && !facingRight)) && !attacking)
        // {
        //     facingRight = !facingRight;
        //     spriteRenderer.flipX = !spriteRenderer.flipX;
        // }
        moveDirection = direction.normalized;
    }

    public void setSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public virtual void Die()
    {
        // play the dying animation 
        // play dying sound for this unit
    }


    public void Knockback(float knockbackPower, Transform obj)
    {
        Move(Vector2.zero);
        Vector2 direction = (obj.transform.position - this.transform.position).normalized;
        rb.AddForce(direction * knockbackPower, ForceMode2D.Impulse);
    }


}
