using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
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

        float moveX = 0;
        float moveY = 0;
        if (dist <= attackRange) // Within melee ranged
        {
            //base.Move(Vector2.zero);
            //stunned = true;
            aiAbility.RequestUse(new InputAction.CallbackContext(), playerPos.position);
            //stunned = false;
        }
        else
        {
            moveX = getMoveX();
            moveY = getMoveY();
        }
        base.Move(new Vector2(moveX, moveY));
        return true;
    }

}
