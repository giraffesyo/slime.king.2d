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

    private Animator anim;

    public virtual bool facingRight
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
    public bool shouldBeAbleToMove = true;
    protected SpriteRenderer spriteRenderer;
    public CapsuleCollider2D meleeCollider; // probably shouldnt be here

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        SetMaxHealth(initialMaxHealth);
        baseSpeed = moveSpeed;
        originalColor = GetComponent<SpriteRenderer>().color;
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
            rb.angularVelocity = moveSpeed;
        }
        // Fixes rotation getting stuck
        if (attacking && anim != null && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            attacking = false;
        }

        // Movement
    }

    public void Move(Vector2 direction)
    {
        if (!attacking && !stunned)
        {
            if (direction.x > 0)
                facingRight = true;
            else if (direction.x < 0)
                facingRight = false;
            spriteRenderer.flipY = false;
            transform.rotation = Quaternion.identity;
        }
        if (shouldBeAbleToMove)
            moveDirection = direction.normalized;
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

    public virtual void Knockback(float knockbackPower, Transform obj)
    {
        StartCoroutine(doKnockback(knockbackPower, obj));
    }

    public IEnumerator doKnockback(float knockbackPower, Transform obj)
    {
        if (isInvincible)
            yield break;


        bool wasStunned = stunned;
        stunned = true;

        beingKnockedBack = true;

        Vector2 direction = (obj.transform.position - this.transform.position).normalized;
        rb.velocity = -direction.normalized * moveSpeed;

        yield return new WaitForSecondsRealtime(knockbackPower);

        beingKnockedBack = false;
        // if they weren't stunned before the knockback, unstun them
        if (!wasStunned)
            stunned = false;
        Move(Vector2.zero);
    }
}