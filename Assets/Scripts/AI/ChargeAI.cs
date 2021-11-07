using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ChargeAI : AI
{
    protected override void Start()
    {
        base.Start();
    }

    protected override bool Move()
    {
        if (!base.Move())
            return false;

        Collider2D[] playerCollider = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        if (playerCollider.Length != 0) // Within melee ranged
        {
            base.Move(Vector2.zero);

            if (aiAbility.RequestUse(new InputAction.CallbackContext(), playerPos.position - transform.position))
                attacking = true;
        }
        else
        {
            base.Move(new Vector2(getMoveX(), getMoveY()));
        }
        return true;
    }

    public override void TakeDamage(int damage)
    {
        // In ChargeAbility, stopCharging only gets called when bull collides with something
        // When hit by slapAbility, there is no collision, slapability works with overlapcollider which means stopCharging will not be called
        // Temporary fix until i think of something better

        ChargeAbility ca = GetComponent<ChargeAbility>();
        if (ca != null)
        {
            ca.stopCharging();
        }
        base.TakeDamage(damage);

    }

}
