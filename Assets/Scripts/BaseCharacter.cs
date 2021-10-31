using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseCharacter : Damageable
{
    public bool beingKnockedBack = false;
    protected float baseSpeed;

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
        baseSpeed = moveSpeed;
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
        if (!beingKnockedBack)
        {
            rb.velocity = moveDirection * moveSpeed;
        }
        // Movement
    }

    public void Move(Vector2 direction, bool shouldBeAttacking = false)
    {
        if (!attacking && !stunned)
        {
            if (direction.x > 0)
                facingRight = true;
            else if (direction.x < 0)
                facingRight = false;
            spriteRenderer.flipY = false;
            transform.rotation = Quaternion.identity;
            moveDirection = direction.normalized;
        }
        if (shouldBeAttacking)
        {
            attacking = true;
        }

    }

    public void setSpeed(float speed)
    {
        moveSpeed = speed;
    }
    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }

    public virtual void Die()
    {
        // play the dying animation 
        // play dying sound for this unit
    }

    public void Knockback(float knockbackPower, Transform obj)
    {
        StartCoroutine(doKnockback(knockbackPower, obj));
    }

    public IEnumerator doKnockback(float knockbackPower, Transform obj)
    {
        bool wasStunned = stunned;
        stunned = true;

        beingKnockedBack = true;


        Vector2 direction = (obj.transform.position - this.transform.position).normalized;
        rb.velocity = -direction.normalized * moveSpeed;

        yield return new WaitForSeconds(knockbackPower);

        beingKnockedBack = false;
        // if they weren't stunned before the knockback, unstun them
        if (!wasStunned)
            stunned = false;
        Move(Vector2.zero);
    }
}