using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThornyAI : AI
{
    Vector2 targetLocation;
    BaseCharacter baseChar;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        baseChar = GetComponent<BaseCharacter>();
    }

    protected override bool Move()
    {
        if (!base.Move())
            return false;

        float moveX = 0;
        float moveY = 0;
        moveX = getMoveX();
        moveY = getMoveY();

        base.Move(new Vector2(moveX, moveY));
        return true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!engulfable)
            hitEnemy(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!engulfable)
            hitEnemy(collision);
    }

    private void hitEnemy(Collision2D collision)
    {
        BaseCharacter enemyChar = collision.gameObject.GetComponent<BaseCharacter>();
        if (enemyChar != null && !enemyChar.attacking)
        {
            enemyChar.TakeDamage(1);
        }
    }
}
