using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKingAi : AI
{

    int cooldownTimer = 5;
    bool isOnCooldown = false;

    SummonAbility summon;
    DebreeAttack debree;
    RangedAttack ranged;
    PolygonCollider2D polyCollider;
    SpriteRenderer sr;
    
    int attackCounter = 0; // Next attack that will be done
    int stage = 1;

    SpriteRenderer meleeAOE_sr;
    CapsuleCollider2D meleeAOE_Collider;
    ContactFilter2D enemyFilter;

    protected override void Start()
    {
        base.Start();
        summon = GetComponent<SummonAbility>();
        debree = GetComponent<DebreeAttack>();
        ranged = GetComponent<RangedAttack>();
        polyCollider = GetComponent<PolygonCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        moveSpeed = 1f;

        meleeAOE_sr = transform.Find("meleeAOE").GetComponent<SpriteRenderer>();
        meleeAOE_Collider = transform.Find("meleeAOE").GetComponent<CapsuleCollider2D>();
        enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Player"));
    }

    protected override bool Move()
    {
       
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
        meleeAOE_sr.enabled = true;
        List<Collider2D> hitEnemies = new List<Collider2D>();
        Physics2D.OverlapCollider(meleeAOE_Collider, enemyFilter, hitEnemies);

        foreach (Collider2D enemy in hitEnemies)
        {
            BaseCharacter enemyChar = enemy.gameObject.GetComponent<BaseCharacter>();

            if (enemyChar != null)
            {
                enemyChar.Knockback(0.3f, this.transform);

                enemyChar.TakeDamage(1);
            }
        }
        StartCoroutine(disableMeleeAOE()); // To show player aoe radius
    }
    IEnumerator disableMeleeAOE()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        meleeAOE_sr.enabled = false;
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
