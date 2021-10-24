using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ChargeAbility : Ability
{

    private PolygonCollider2D chargeCollider;
    private ContactFilter2D enemyFilter;
    private Vector2 direction;
    private bool isCharging = false;

    public BaseCharacter baseChar;
    public int attackDamage = 2;

    override protected void Start()
    {
        base.Start();
        abilityKey = Ability.AbilityKey.Charge;
        enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(enemyLayers);
    }
    public override bool RequestUse(InputAction.CallbackContext ctx, Vector2 aimingDirection)
    {
        if (!onCooldown && animator != null && !locked)
        {
            direction = aimingDirection;
            rotation = Mathf.Atan2(aimingDirection.y, aimingDirection.x) * Mathf.Rad2Deg;
            animator.SetBool("Charging", true);
            animator.SetTrigger("Charge");
            return true;
        }
        return false;
    }

    override public void Use(int key)
    {
        if (key != (int)this.abilityKey)
            return;

        if (onCooldown)
        {
            return;
        }
        base.Use(key);

        if (chargeCollider == null)
        {
            chargeCollider = gameObject.AddComponent<PolygonCollider2D>();
            chargeCollider.isTrigger = true;
        }

        baseChar.Move(direction);
        baseChar.setSpeed(10);
        isCharging = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Implement if gets hit stop charging
        if (isCharging)
        {
            BaseCharacter enemyChar = collision.transform.GetComponent<BaseCharacter>();
            if (enemyChar != null && !enemyChar.invincible)
            {
                enemyChar.TakeDamage(attackDamage);
                if (!isAi)
                    StartCoroutine(enemyChar.Knockback(2f, GetComponent<Transform>().transform));
            }

            isCharging = false;
            baseChar.Move(Vector2.zero);
            baseChar.setSpeed(2);
            animator.SetBool("Charging", false);
            baseChar.stunned = false;
            baseChar.Knockback(3, collision.transform);
        }
    }
}