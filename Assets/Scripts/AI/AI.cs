using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class AI : BaseCharacter
{
    public Transform attackPoint;
    protected Ability aiAbility;

    protected Transform playerPos;    // Transform object of the controllable player
    protected float dist;
    public float attackRange;
    public bool alwaysEngulfable;

    public LayerMask playerLayer;
    public LayerMask solidObjectsLayer;
    protected Animator animator;

    static protected float HPMult;
    protected float maxHealthNew;

    Transform stunObject;
    bool engulfStunned = false;
    virtual public bool engulfable
    {
        get
        {
            return alwaysEngulfable || engulfStunned;
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        aiAbility = GetComponent<Ability>();
        animator = GetComponent<Animator>();
        stunObject = transform.Find("StunnedObject");
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        playerLayer = LayerMask.GetMask("Player");
        solidObjectsLayer = LayerMask.GetMask("SolidObjects");

        setSpeed(baseSpeed*PlayerPrefs.GetFloat("EnemySpeed", 1.0f));
        HPMult = PlayerPrefs.GetFloat("EnemyHPMult", 1.0f);
        if (HPMult != 1.0f)
        {
          maxHealthNew = ((float)_maxHealth)*HPMult;
          Mathf.RoundToInt(maxHealthNew);
          if (maxHealthNew < 1.0f)
          {
            maxHealthNew = 1.0f;
          }
          SetMaxHealth((int)maxHealthNew);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Louis" && currentHealth == 1)
        {
            StartCoroutine(Stun(1800));
        }
        //StartCoroutine(Move());
        Move();
    }

    protected virtual bool Move()
    {
        if (engulfStunned || stunned || attacking || beingKnockedBack)
            return false;

        dist = Vector3.Distance(playerPos.position, transform.position);

        if (dist >= 15f || !inLineofSight())    // Out of character sight range
            return false;

        return true;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (currentHealth == 1)
        {
            StartCoroutine(Stun(3));
        }
    }

    IEnumerator Stun(int time)
    {
        if (stunObject != null)
        {

            //base.Move(new Vector2(0, 0));
            stunObject.GetComponent<SpriteRenderer>().enabled = true;
            engulfStunned = true;
            transform.rotation = Quaternion.identity;
            spriteRenderer.flipY = false;

            yield return new WaitForSecondsRealtime(time);
            SetCurrentHealth(currentHealth + 1);
            engulfStunned = false;
            attacking = false;
            stunObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public override void Die()
    {
        base.Die();
        // Destroy the AI
        Destroy(this.gameObject);
    }

    protected virtual bool inLineofSight()
    {
        float dist = Vector3.Distance(transform.position, playerPos.position);
        Vector3 direction = (playerPos.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, dist, solidObjectsLayer);

        if (hit.collider == null)
            return true;

        return false;

    }

    protected int getMoveX()
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
    protected int getMoveY()
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

}
