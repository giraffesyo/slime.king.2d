using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SmallBatAI : AI
{
    Vector2 targetLocation;
    BaseCharacter baseChar;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        alwaysEngulfable = true;
        baseChar = GetComponent<BaseCharacter>();
    }


    override protected bool inLineofSight()
    {
        return true;
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
        hitEnemy(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
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
