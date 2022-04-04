using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class DrillAi : AI
{
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

        if (playerCollider.Length != 0) // Within range of the drill
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
        // In DrillAbility, stopDrilling only gets called when bat collides with something
        // When hit by slapAbility, there is no collision, slapability works with overlapcollider which means stopDrilling will not be called
        // Temporary fix until i think of something better

        DrillAbility da = GetComponent<DrillAbility>();
        if (da != null)
        {
            da.stopDrilling();
        }
        base.TakeDamage(damage);

    }

}
