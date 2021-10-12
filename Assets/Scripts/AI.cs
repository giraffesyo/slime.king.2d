using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIType
{
    NONE,
    MELEE,
    RANGED,
}

public class AI : BaseCharacter
{
    public AIType aiMode;
    Ability aiAbility;

    Transform playerPos;    // Transform object of the controllable player
    float attackRange;
    float playerRange;     // For ranged ai, how close the player will be before walking backwards

    public LayerMask playerLayer;

    Transform stunObject;
    bool engulfStunned = false;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        aiAbility = GetComponent<Ability>();
        stunObject = transform.GetChild(1);
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        playerLayer = LayerMask.GetMask("Player");
        if (aiMode == AIType.RANGED)
        {
            attackRange = 4f;
            playerRange = 3.8f;
            setSpeed(3f);
        }
        else if (aiMode == AIType.MELEE)
        {

            attackRange = .65f;
            setSpeed(2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move());
    }

    protected IEnumerator Move()
    {
        if (engulfStunned)
            yield break;

        float x1 = transform.position.x;
        float y1 = transform.position.y;

        float x2 = playerPos.position.x;
        float y2 = playerPos.position.y;
        float dist = Distance(x1, y1, x2, y2);

        if (dist >= 10f)    // Out of character sight range
            yield break;


        moveX = 0;
        moveY = 0;

        Collider2D[] playerCollider = Physics2D.OverlapCircleAll(attackPoint.position, attackRange - .2f, playerLayer);

        if (aiMode == AIType.MELEE)
        {
            if (playerCollider.Length != 0) // Within melee ranged
            {
                base.Move(0, 0);
                stunned = true;

                yield return new WaitForSecondsRealtime(1.0f);

                stunned = false;
                aiAbility.Use(0);
            }
            else
            {
                moveX = getMoveX();
                moveY = getMoveY();
            }                
            moveAttackPoint();
        }
        else if (aiMode == AIType.RANGED) // RANGED
        {
            if (playerCollider.Length == 0) // Gotta get closer
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
        }
        else if (aiMode == AIType.NONE) // Just walks towards character
        {
            moveX = getMoveX();
            moveY = getMoveY();
        }
        base.Move(moveX, moveY);
    }

    float Distance(float x1, float y1, float x2, float y2)
    {
        return Mathf.Sqrt(Mathf.Pow(x2 - x1, 2) + Mathf.Pow(y2 - y1, 2)); ;
    }

    int getMoveX()
    {
        float diff = Mathf.Abs(transform.position.x - playerPos.position.x);
        if (diff > 0.1f && transform.position.x < playerPos.position.x)
        {
            return 1;
        }
        else if (diff > 0.1f && transform.position.x > playerPos.position.x)
        {
            return -1;
        }
        return 0;
    }
    int getMoveY()
    {
        float diff = Mathf.Abs(transform.position.y - playerPos.position.y);
        if (diff > 0.1f && transform.position.y < playerPos.position.y)
        {
            return 1;
        }
        else if (diff > 0.1f && transform.position.y > playerPos.position.y)
        {
            return -1;
        }
        return 0;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (currentHealth == 1)
        {
            StartCoroutine(Stun());
        }
    }

    IEnumerator Stun()
    {
        base.Move(0, 0);
        stunObject.GetComponent<SpriteRenderer>().enabled = true;
        engulfStunned = true;
        
        yield return new WaitForSecondsRealtime(3.0f);

        engulfStunned = false;
        stunObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    protected override void Die()
    {
        base.Die();
        // drop some coins!
        // Destroy the AI
        Destroy(this.gameObject);
    }
}
