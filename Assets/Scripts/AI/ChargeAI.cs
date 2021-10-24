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

            if(aiAbility.RequestUse(new InputAction.CallbackContext(), playerPos.position - transform.position))
                stunned = true;
        }
        else
        {
            moveX = getMoveX();
            moveY = getMoveY();
            base.Move(new Vector2(moveX, moveY));
        }
        
        return true;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        ChargeAbility ca = GetComponent<ChargeAbility>();
        if (ca != null)
        {
            ca.stopCharging();
        }
    }

}
