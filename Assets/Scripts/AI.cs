using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIType
{
    MELEE,
    RANGED
}

public class AI : BaseCharacter
{
    Transform playerPos;    // Transform object of the controllable player
    public AIType aiMode;

    Combat aiCombat;
    int cooldownCounter = 0;
    int attackCooldown;
    float attackRange;
    float playerRange;     // For ranged ai, how close the player will be before walking backwards
    public LayerMask playerLayer;


    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        aiCombat = GetComponent<Combat>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        playerLayer = LayerMask.GetMask("Player");
        if (aiMode == AIType.RANGED)
        {
            attackRange = 4f;
            playerRange = 3.8f;
            setSpeed(3f);
            attackCooldown = 100;
        }
        else if (aiMode == AIType.MELEE)
        {
            attackRange = 1f;
            setSpeed(2f);
            attackCooldown = 50;
        }
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();
        if (cooldownCounter != 0)
        {
            cooldownCounter = (cooldownCounter + 1) % attackCooldown;   // fixedUpdate gets called 50 times per second
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected void Move()
    {
        moveX = 0;
        moveY = 0;

        Collider2D[] playerCollider = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        if (aiMode == AIType.MELEE && cooldownCounter == 0)
        { // MELEE
            if (playerCollider.Length != 0) // Within melee ranged
            {
                cooldownCounter = 1;
                aiCombat.MeleeAttack();
            }
            if (attackCooldown != 0)
            {
                moveX = getMoveX();
                moveY = getMoveY();
                moveAttackPoint();
            }
        }
        else if (aiMode == AIType.RANGED && playerCollider.Length != 0) // RANGED
        {
            Transform playerTransform = playerCollider[0].GetComponent<Transform>();
            float x1 = transform.position.x;
            float y1 = transform.position.y;

            float x2 = playerTransform.position.x;
            float y2 = playerTransform.position.y;
            if (Distance(x1, y1, x2, y2) < playerRange)
            {
                moveX = getMoveX() * -1;
                moveY = getMoveY() * -1;
            }
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
    protected override void Die()
    {
        base.Die();
        // drop some coins!
    }
}
