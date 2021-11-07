using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnightAI : AI
{
    Vector2 targetLocation;
    BaseCharacter baseChar;

    public bool teleport;  // Used for debugging, delete later

    bool hitWall;

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


        if (moveDirection == Vector2.zero)
        {
            targetLocation = getTargetLocation();
            base.Move(targetLocation);
            targetLocation = targetLocation + (Vector2)transform.position;

        }
        else if (Vector2.Distance((Vector2)transform.position, targetLocation) < .1f || hitWall)
        {
            hitWall = false;
            moveDirection = Vector2.zero;
        }
        if (!aiAbility.onCooldown)
            StartCoroutine(DoAbility());
        return true;
    }

    private Vector2 getTargetLocation()
    {
        /* The way this works:
         * X     X 
         *  \   /
         *   \ /        X = Possible location
         *    O         O = Ooey
         *    |         K = Knight
         *    |
         *    K     
         * I wanted knight slime to be close to the player without having to constantly chase the player. (Bc then it would be hard to hit without taking dmg)
         * Since knight slime also walks in a straight line its predictable/easier to hit him.
         * Knight slime can also get in the way when player is trying to move/dodge adding a bit of difficulty to dealing with knight slime.
         *    
        */

        // Get vector from knight to ooey
        Vector2 playerDirection = (Vector2)(playerPos.position - transform.position);

        float angle = 15 * Mathf.Deg2Rad;
        // Choosing left or right side of ooey
        if (Random.Range(0f, 2f) < 1f)
        { // Left side of ooey 
            angle *= -1;
        }

        // Rotation Matrix
        Vector2 rotatedVector = new Vector2(
            (playerDirection.x * Mathf.Cos(angle)) - (playerDirection.y * Mathf.Sin(angle)),
            (playerDirection.x * Mathf.Sin(angle)) + (playerDirection.y * Mathf.Cos(angle)));

        // Makes magnitude of vector to 3
        Vector2 size3Vector = (3f/rotatedVector.magnitude) * rotatedVector;

        return size3Vector + playerDirection;
    }

    private IEnumerator DoAbility()
    {
        animator.SetTrigger("Block");
        yield return new WaitForSecondsRealtime(0.1f);
        aiAbility.Use((int)Ability.AbilityKey.Block);
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
        hitWall = true;

        BaseCharacter enemyChar = collision.gameObject.GetComponent<BaseCharacter>();
        if (enemyChar != null)
        {
            enemyChar.Knockback(0.8f, this.transform);

            enemyChar.TakeDamage(1);
        }
    }
}
