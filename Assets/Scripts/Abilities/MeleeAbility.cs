using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MeleeAbility : Ability
{

    public Transform attackPoint;
    public float attackRange = .65f;
    public int attackDamage = 1;
    Vector2 aimingDirection;
    float rotation;
    bool flipped;

    public override void RequestUse(InputAction.CallbackContext ctx, Vector2 aimingDirection)
    {
        if (!onCooldown && animator != null)
        {
            if (animator != null)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(aimingDirection);
                this.aimingDirection = new Vector2(worldPos.x - transform.position.x, worldPos.y - transform.position.y).normalized;
                rotation = Mathf.Atan2(this.aimingDirection.y, this.aimingDirection.x) * Mathf.Rad2Deg;
                animator.SetTrigger("Melee");
            }
        }
    }

    override public void Use(int key)
    {
        if (key != (int)Ability.BasicAbilityKeys.Melee)
            return;

        if (onCooldown)
        {
            return;
        }
        base.Use(key);

        // Temporary, flickers white circle showing hitboxes of attacks
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Swipe");


        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            BaseCharacter enemyChar = enemy.GetComponent<BaseCharacter>();
            if (!enemyChar.invincible)
            {
                enemyChar.TakeDamage(attackDamage);
                if (!isAi)
                    StartCoroutine(enemyChar.Knockback(2f, GetComponent<Transform>().transform));
            }
        }
        //transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    // This gets called from an animation event at first frame and last frame
    public void Rotate(int firstFrame)
    {            
        bool facingRight = GetComponent<Player>().facingRight;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float rt = rotation;
        if (firstFrame == 0)
        {
            if (!facingRight)
            {
                sr.flipX = !sr.flipX;
                flipped = true;
            }
        }
        else
        {
            if (flipped)
                sr.flipX = !sr.flipX;
            flipped = false;
            rt = rt * -1;
        }

        if (rt > 90f || rt < -90f)
            sr.flipY = !sr.flipY;    
        transform.Rotate(new Vector3(0, 0, 1), rt);
    }

    // For debugging. Draws circle when in editing mode showing attack range (Must click on ooey to see circle)
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            Debug.Log("Attack point is null");

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
