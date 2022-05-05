using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrillAbility : Ability
{
    [SerializeField] int attackDamage = 1;

    private Vector2 direction;
    private bool isDrilling = false;
    private Vector3 startedFrom;
    private bool stopping;

    private BaseCharacter baseChar;
    public float drillRange;
    private Rigidbody2D rb;
    private bool alreadyHit;
    private Transform ooey;



    override protected void Start()
    {
        base.Start();
        stopping = false;
        abilityKey = Ability.AbilityKey.Drill;
        baseChar = GetComponent<BaseCharacter>();
        rb = GetComponent<Rigidbody2D>();
        alreadyHit = false;
        ooey = FindObjectOfType<Player>().transform;
    }

    private void FixedUpdate()
    {
        if (isDrilling)
        {
            if ((transform.position - startedFrom).sqrMagnitude > drillRange && !stopping)
            {
                stopping = true;
                Debug.Log("Signalling stop to drilling because of range");
                StartCoroutine(stopDrillingAfterTime(0.2f));

            }
        }
    }

    public override bool RequestUse(InputAction.CallbackContext ctx, Vector2 mousePosition)
    {
        if (!onCooldown && animator != null)
        {
            Vector2 aimingDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            direction = aimingDirection;
            rotation = Mathf.Atan2(aimingDirection.y, aimingDirection.x) * Mathf.Rad2Deg;
            animator.SetBool("Drilling", true);
            animator.SetTrigger("Drill");
            return true;
        }
        return false;
    }

    public void stopDrilling()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.freezeRotation = true;
        animator.SetBool("Drilling", false);
        isDrilling = false;
        //baseChar.Move(Vector2.zero);
        baseChar.ResetSpeed();
        baseChar.attacking = false;
        baseChar.shouldBeAbleToMove = true;
    }

    override public void Use(int key)
    {
        if (key != (int)this.abilityKey)
            return;

        if (onCooldown)
        {
            return;
        }

        if (baseChar.stunned)
        {
            stopDrilling();
            return;
        }
        baseChar.attacking = true;
        alreadyHit = false;
        stopping = false;
        rb.freezeRotation = false;
        // transform.LookAt(ooey);
        Vector3 targ = ooey.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        if (isAi)
        {
            spriteRenderer.flipX = true;
        }

        startedFrom = transform.position;
        base.Use(key);

        // baseChar.setSpeed(10);
        // baseChar.Move(direction, shouldBeAttacking: true);
        baseChar.shouldBeAbleToMove = false;
        baseChar.moveDirection = direction.normalized * 5;

        isDrilling = true;
    }

    private IEnumerator stopDrillingAfterTime(float time)
    {

        yield return new WaitForSeconds(time);
        stopping = false;
        if (isDrilling)
        {
            stopDrilling();
            rb.angularVelocity = 0;
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Drill Collided with " + collision.gameObject.name);
        if (isDrilling && !alreadyHit)
        {
            checkHit(collision.transform);
        }
    }

    private void checkHit(Transform transform)
    {
        BaseCharacter enemyChar = transform.GetComponent<BaseCharacter>();
        if (enemyChar != null && !enemyChar.isInvincible)
        {
            alreadyHit = true;
            enemyChar.TakeDamage(attackDamage);
            enemyChar.Knockback(knockbackPower: 1f, transform);
        }
    }
}