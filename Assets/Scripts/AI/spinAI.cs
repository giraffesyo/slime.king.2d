using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class spinAI : AI
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    override public bool facingRight
    {
        get
        {
            return spriteRenderer.flipX;
        }
        set
        {
            spriteRenderer.flipX = value;
        }
    }

    protected override bool Move()
    {
        if (!base.Move())
            return false;

        Collider2D[] playerCollider = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        if (playerCollider.Length != 0) // Within melee ranged
        {
            // don't keep moving
            base.Move(Vector2.zero);

            if (aiAbility.RequestUse(new InputAction.CallbackContext(), playerPos.position))
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

        spinAbility sa = GetComponent<spinAbility>();
        if (sa != null)
        {
            sa.stopSpinning();
        }
        base.TakeDamage(damage);

    }
}
