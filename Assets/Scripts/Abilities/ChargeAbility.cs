using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChargeAbility : Ability
{
    [SerializeField] int attackDamage = 2;

    private Vector2 direction;
    private bool isCharging = false;

    private BaseCharacter baseChar;

    override protected void Start()
    {
        base.Start();
        abilityKey = Ability.AbilityKey.Charge;
        baseChar = GetComponent<BaseCharacter>();
    }
    public override bool RequestUse(InputAction.CallbackContext ctx, Vector2 aimingDirection)
    {
        if (!onCooldown && animator != null)
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

        animator.SetBool("Charging", false);
        isCharging = false;
        //baseChar.Move(Vector2.zero);
        baseChar.ResetSpeed();

        baseChar.shouldBeAbleToMove = true;
    }

    override public void Use(int key)
    {
        if (key != (int)this.abilityKey)
            return;

        if (onCooldown)
        {
            return;
        }

        if (baseChar.stunned)
        {
            stopCharging();
            return;
        }

        base.Use(key);

        // baseChar.setSpeed(10);
        // baseChar.Move(direction, shouldBeAttacking: true);
        baseChar.shouldBeAbleToMove = false;
        baseChar.moveDirection = direction.normalized * 10;

        isCharging = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isCharging)
        {
            checkHit(collision);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Fixes bug where if touching wall will always collide even though not aiming for wall
        Vector2 dirVector = direction;
        Vector2 colVector = (Vector2)(collision.transform.position) - (Vector2)(transform.position);
        float angle = Mathf.Atan2(colVector.y - dirVector.y, colVector.x - dirVector.x) * Mathf.Rad2Deg;

        if (isCharging && Mathf.Abs(angle) >= 90f)
        {
            checkHit(collision);
        }
    }

    private void checkHit(Collision2D collision)
    {
        BaseCharacter enemyChar = collision.transform.GetComponent<BaseCharacter>();
        if (enemyChar != null && !enemyChar.invincible)
        {
            enemyChar.TakeDamage(attackDamage);
            enemyChar.Knockback(knockbackPower: 1f, transform);
        }

        stopCharging();

        baseChar.Knockback(knockbackPower: 0.5f, collision.transform);
    }
}