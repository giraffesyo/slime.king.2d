using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    Transform greenHP;
    float scale;
    float decreaseBy;

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
        greenHP = transform.Find("GreenHP");
        scale = greenHP.localScale.x;
        decreaseBy = scale / initialMaxHealth;
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
        if (!isOnCooldown && !attacking)
        {
            base.Move(new Vector2(0,0));
            switch (attackCounter)
            {
                case 0:
                    moveSpeed = 5f;
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
        moveSpeed = 1f;
    }

    public override void TakeDamage(int damage)
    {        
        // Transform green hp
        float prevScale = scale;
        scale -= decreaseBy;

        greenHP.localPosition = greenHP.localPosition - new Vector3((prevScale - scale) / 2, 0, 0);
        greenHP.localScale = new Vector3(scale, 0.2f, 1);
        

        if(currentHealth == 1)  // Dont want object to be destroyed before doing animation
        {
            polyCollider.enabled = false;
            StartCoroutine(DeathAnimation());
            return;
        }
        base.TakeDamage(damage);



        if(currentHealth % 5 == 0) // 5, 10, 15
        {
            base.Move(new Vector2(0, 0));
            attacking = true;
            animator.SetTrigger("Tired");
            summon.Use(stage);
            stage++;
            StartCoroutine(NotTired());
        }
    }

    IEnumerator DeathAnimation()
    {
        attacking = true;
        base.Move(new Vector2(0, 0));
        

        //SpriteRenderer sr = GetComponent<SpriteRenderer>();
       
        float amount = 255;
        float decreaseBy = 255 / 30;

        for(int i = 0; i < 30; i++)
        {
            amount -= decreaseBy;
            sr.color = new Color(1, 1, 1, amount/255);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        sr.color = new Color(1, 1, 1, 0);


        yield return new WaitForSecondsRealtime(2f);
        Destroy(gameObject);
        SceneManager.LoadScene("OoeysReign", LoadSceneMode.Single);
        // Do endgame scene
    }

    IEnumerator NotTired()
    {
        yield return new WaitForSecondsRealtime(5f);
        animator.SetTrigger("Not Tired");
        attacking = false;
    }

    bool inPosition()
    {
        Debug.Log($"Position: {playerPos.position} X: {topLeftX} {botRightX} Y: {topLeftY} {botRightY} ");
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

    public override void Knockback(float knockbackPower, Transform obj)
    {
       // Do Nothing, slime king shouldnt be knocked back
    }
}
