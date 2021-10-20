using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
    public bool beingKnockedBack;
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
        rb.velocity = moveDirection * moveSpeed;
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


    public IEnumerator Knockback(float knockbackPower, Transform obj)
    {
        stunned = true;
        beingKnockedBack = true;
        Move(Vector2.zero);
        Vector2 direction = (obj.transform.position - this.transform.position).normalized;
        transform.DOMove(new Vector3(transform.position.x - (direction.x * knockbackPower), transform.position.y - (direction.y * knockbackPower), 0), 0.5f).OnComplete(() => beingKnockedBack = false);

        stunned = false;
        yield return 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (beingKnockedBack)
        {
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
