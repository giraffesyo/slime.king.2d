using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BaseCharacter : Damageable
{
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
    public bool beingKnockedBack;
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

    public void Move(Vector2 direction)
    {
        if (stunned)
        {
            // moveDirection = Vector2.zero;  // Messes with charge ability
            //return;
        }
        if (!attacking && !stunned)
        {
            if (direction.x > 0)
                facingRight = true;
            else if (direction.x < 0)
                facingRight = false;
            //facingRight = direction.x > 0 ? true : false; // Changed it to if else bc if direction.x = 0 it will automatically turn right when standing still (dont know how to do = 0 in this format lol)
            spriteRenderer.flipY = false;
            transform.rotation = Quaternion.identity;
        }
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
        Move(knockbackPower * -direction);

        yield return new WaitForSeconds(0.5f);

        Move(Vector2.zero);
        //transform.DOMove(new Vector3(transform.position.x - (direction.x * knockbackPower), transform.position.y - (direction.y * knockbackPower), 0), 0.5f).OnComplete(() => beingKnockedBack = false);
        beingKnockedBack = false;
        // if they weren't stunned before the knockback, unstun them
        if (!wasStunned)
            stunned = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (beingKnockedBack)
        {
            beingKnockedBack = false;
            // check if the collision is a solid object
            // maybe a better way to do this but lets see if we can get this to work...
            if (other.gameObject.layer == 9)
            { // "solid objects" is 9
              // stop ongoing tweens
                transform.DOKill(false);

            }
        }
    }
}
