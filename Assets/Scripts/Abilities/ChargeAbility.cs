using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ChargeAbility : Ability
{

    private PolygonCollider2D chargeCollider;
    private Vector2 direction;
    private bool isCharging = false;

    public BaseCharacter baseChar;
    public int attackDamage = 2;

    override protected void Start()
    {
        base.Start();
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

    public void stopCharging()
    {
        Debug.Log("Stop charging");
        animator.SetBool("Charging", false);
        isCharging = false;
        baseChar.Move(Vector2.zero);
        baseChar.ResetSpeed();

        baseChar.stunned = false;
    }

    override public void Use(int key)
    {
        if (key != (int)Ability.BasicAbilityKeys.Charge)
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

        baseChar.setSpeed(10);
        baseChar.Move(direction);
        
        isCharging = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isCharging)
        {
            BaseCharacter enemyChar = collision.transform.GetComponent<BaseCharacter>();
            if (enemyChar != null && !enemyChar.invincible)
            {
                enemyChar.TakeDamage(attackDamage);
                if (!isAi)
                    enemyChar.Knockback(2f, GetComponent<Transform>().transform);
            }

            stopCharging();
            baseChar.Knockback(3, collision.transform);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Fixes bug where if touching wall will always collide even though not aiming for wall
        Vector2 dirVector = direction;
        Vector2 colVector = (Vector2)(collision.transform.position) - (Vector2)(transform.position);
        float angle = Mathf.Atan2(colVector.y - dirVector.y,colVector.x - dirVector.x) * Mathf.Rad2Deg;

        if (isCharging && Mathf.Abs(angle) < 90) // Walking into wall
        {
            BaseCharacter enemyChar = collision.transform.GetComponent<BaseCharacter>();
            if (enemyChar != null && !enemyChar.invincible)
            {
                enemyChar.TakeDamage(attackDamage);
                if (!isAi)
                    enemyChar.Knockback(5f, GetComponent<Transform>().transform);
            }

            stopCharging();
            baseChar.Knockback(3, collision.transform);
        }
    }
}