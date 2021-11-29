using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKingAi : AI
{
    // Room Coordinates
    [SerializeField] float topLeftX;
    [SerializeField] float topLeftY;
    [SerializeField] float botRightX;
    [SerializeField] float botRightY;
    
    

    int cooldownTimer = 5;
    bool isOnCooldown = false;
    
    SummonAbility summon;
    DebreeAttack debree;
    RangedAttack ranged;
    MeleeAttack melee;
    PolygonCollider2D polyCollider;
    SpriteRenderer sr;
    
    int attackCounter = 0; // Next attack that will be done
    int stage = 1;


    protected override void Start()
    {
        base.Start();
        summon = GetComponent<SummonAbility>();
        debree = GetComponent<DebreeAttack>();
        ranged = GetComponent<RangedAttack>();
        melee = GetComponent<MeleeAttack>();
        polyCollider = GetComponent<PolygonCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        moveSpeed = 1f;

        debree.setCoords(topLeftX, botRightX, botRightY, topLeftY);
    }

    protected override bool Move()
    {
        if (!inPosition())
        {
            base.Move(new Vector2(0, 0));
            return false;
        }
       
        DoAttack();
       
        if(!attacking)
            base.Move(new Vector2(getMoveX(), getMoveY()));

        return true;
    }

    void DoAttack()
    {
        if (!isOnCooldown)
        {
            base.Move(new Vector2(0,0));
            switch (attackCounter)
            {
                case 0:
                    animator.SetTrigger("Melee");
                    break;
                case 1:
                    animator.SetTrigger("Jump");
                    break;
                case 2:
                    attacking = true;
                    animator.SetTrigger("Ranged");
                    break;
                default:
                    Debug.Log($"Attack counter out of range: {attackCounter}");
                    break;
            }
            attackCounter = (attackCounter + 1) % 3;

            isOnCooldown = true;
            StartCoroutine(StartCooldown());    // Cooldown until next attack can be used
        }
    }
    IEnumerator StartCooldown()
        {
            yield return new WaitForSecondsRealtime(cooldownTimer);
            isOnCooldown = false;
        }

    void doDebreeAttack()   // Is called by animation event
    {
        attacking = true;
        polyCollider.enabled = false;
        debree.Use();
        StartCoroutine(SlamCountdown());
    }

    IEnumerator SlamCountdown()
    {
        sr.enabled = false;
        yield return new WaitForSecondsRealtime(3f);        
        sr.enabled = true;
        animator.SetTrigger("Slam");
        polyCollider.enabled = true;

        attacking = false;
    }
    void doRangedAttack()   // Animation Event
    {
        ranged.Use();
        attacking = false;
    }

    void doMeleeAttack()    // Animation Event
    {
        melee.Use();
        attacking = false;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if(currentHealth % 5 == 0 && currentHealth != 0) // 5, 10, 15
        {
            base.Move(new Vector2(0, 0));
            attacking = true;
            animator.SetTrigger("Tired");
            summon.Use(stage);
            stage++;
            StartCoroutine(NotTired());
        }
    }
    IEnumerator NotTired()
    {
        yield return new WaitForSecondsRealtime(5f);
        animator.SetTrigger("Not Tired");
        attacking = false;
    }

    bool inPosition()
    {
        if(playerPos.position.x >= topLeftX && playerPos.position.x <= botRightX)
        {
            if (playerPos.position.y >= botRightY && playerPos.position.y <= topLeftY)
            {
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BaseCharacter enemyChar = collision.gameObject.GetComponent<BaseCharacter>();

        if (enemyChar != null && !enemyChar.attacking)
        {
            enemyChar.Knockback(0.3f, this.transform);

            enemyChar.TakeDamage(1);
        }
    }
}
