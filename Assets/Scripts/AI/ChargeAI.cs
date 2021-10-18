using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAI : AI
{
    protected override void Start()
    {
        base.Start();
        attackRange = 1f;
        setSpeed(2f);
    }

    protected override bool Move()
    {
        if (!base.Move())
            return false;

        Collider2D[] playerCollider = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        if (playerCollider.Length != 0) // Within melee ranged
        {
            base.Move(Vector2.zero);
            stunned = true;
            Invoke("unStun", 0.5f);       // Did this so it wait a bit until it attacks
        }
        else
        {
            moveX = getMoveX();
            moveY = getMoveY();
        }
        base.Move(new Vector2(moveX, moveY));
        return true;
    }

    void unStun()
    {
        stunned = false;
        aiAbility.Use(0);
    }
}
