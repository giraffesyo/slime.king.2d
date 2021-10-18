using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MeleeAbility : Ability
{

    private PolygonCollider2D meleeCollider;
    private ContactFilter2D enemyFilter;

    public int attackDamage = 1;
    float rotation;
    bool flipped;
    bool locked;    // Band-aid fix? Prevents from melee being spammed in the short window from when animation starts and Use() is called

    override protected void Start()
    {
        base.Start();
        enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(enemyLayers);
    }
    public override void RequestUse(InputAction.CallbackContext ctx, Vector2 aimingDirection)
    {
        if (!onCooldown && animator != null && !locked)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(aimingDirection);
            aimingDirection = new Vector2(worldPos.x - transform.position.x, worldPos.y - transform.position.y).normalized;
            rotation = Mathf.Atan2(aimingDirection.y, aimingDirection.x) * Mathf.Rad2Deg;
            animator.SetTrigger("Melee");
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

        if (meleeCollider == null)
        {
            meleeCollider = gameObject.AddComponent<PolygonCollider2D>();
            meleeCollider.isTrigger = true;
        }

        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Swipe");

        List<Collider2D> hitEnemies = new List<Collider2D>();

        Physics2D.OverlapCollider(meleeCollider, enemyFilter, hitEnemies);

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
        Player plyr = GetComponent<Player>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float rt = rotation;
        if (firstFrame == 0)
        {
            locked = true;
            if (!plyr.facingRight)
            {
                sr.flipX = !sr.flipX;
                flipped = true;
            }
        }
        else
        {
            locked = false;
            if (flipped)
                sr.flipX = !sr.flipX;
            flipped = false;
            rt = rt * -1;
        }

        if (rt > 90f || rt < -90f)
            sr.flipY = !sr.flipY;    
        transform.Rotate(new Vector3(0, 0, 1), rt);
    }
}
