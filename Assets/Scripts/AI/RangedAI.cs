using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAI : AI
{
    public float playerRange;     // For ranged ai, how close the player will be before walking backwards

    protected override void Start()
    {
        base.Start();
    }

    protected override bool Move()
    {
        if(!base.Move())
            return false;

        float x1 = transform.position.x;
        float y1 = transform.position.y;

        float x2 = playerPos.position.x;
        float y2 = playerPos.position.y;

        float moveX = 0;
        float moveY = 0;


        if (dist > attackRange) // Gotta get closer
        {
            moveX = getMoveX();
            moveY = getMoveY();
        }
        else
        {
            if (dist < playerRange) // Player too close, back away
            {
                moveX = getMoveX() * -1;
                moveY = getMoveY() * -1;
            }
            aiAbility.Use(new Vector2(x2 - x1, y2 - y1).normalized);
        }

        base.Move(new Vector2(moveX, moveY));
        return true;
    }
}
