using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : AI
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override bool Move()
    {
        if (!base.Move())
            return false;

        Collider2D[] playerCollider = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);
        float moveX = 0;
        float moveY = 0;
        if (playerCollider.Length != 0) // Within melee ranged
        {
            //base.Move(Vector2.zero);
            stunned = true;
            //aiAbility.Use(0);
            stunned = false;
        }
        else
        {
            moveX = getMoveX();
            moveY = getMoveY();
        }
        //base.Move(new Vector2(moveX, moveY));
        return true;
    }

}
