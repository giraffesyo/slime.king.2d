using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : AI
{
    // Start is called before the first frame update
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
            aiAbility.Use(0);
            stunned = false;
            //Invoke("unStun", 0.5f);       //causes lag how to add short delay? cant use coroutine bc have to wait for base Move()
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
        aiAbility.Use(0);        
        stunned = false;
    }
}
