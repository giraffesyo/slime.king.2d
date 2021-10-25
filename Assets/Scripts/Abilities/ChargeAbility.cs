using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChargeAbility : Ability
{
    private Vector2 direction;
    private bool isCharging = false;

    public BaseCharacter baseChar;
    public int attackDamage = 2;

    override protected void Start()
    {
        base.Start();
        abilityKey = Ability.AbilityKey.Charge;
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
        animator.SetBool("Charging", false);
        isCharging = false;
        //baseChar.Move(Vector2.zero);
        baseChar.ResetSpeed();

        baseChar.attacking = false;
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

        baseChar.setSpeed(10);
        baseChar.Move(direction);

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

        if (isCharging && Mathf.Abs(angle )>= 90f)
        {
            checkHit(collision);
        }
    }

    private void checkHit(Collision2D collision)
    {
        Debug.Log("Charge hit something");
        BaseCharacter enemyChar = collision.transform.GetComponent<BaseCharacter>();
        if (enemyChar != null && !enemyChar.invincible)
        {
            Debug.Log("Charge hit enemy");

            enemyChar.TakeDamage(attackDamage);
            enemyChar.Knockback(knockbackPower: 5f, transform);
        }

        stopCharging();

        baseChar.Knockback(knockbackPower: 3, collision.transform);
    }
}