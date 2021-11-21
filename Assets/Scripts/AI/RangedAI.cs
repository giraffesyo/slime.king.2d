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

        Rotate(playerPos.position - transform.position);

        
        if (dist < attackRange) // Gotta get closer
            aiAbility.Use(new Vector2(x2 - x1, y2 - y1).normalized);

        return true;
    }

    public void Rotate(Vector2 direction)
    {
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if ((rotation > 90f || rotation < -90f))
            spriteRenderer.flipY = true;
        else
            spriteRenderer.flipY = false;
        transform.localEulerAngles = new Vector3(0, 0, rotation);


    }
}
