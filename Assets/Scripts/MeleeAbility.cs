using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MeleeAbility : Ability
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 1;
    public bool isAi;
    public LayerMask enemyLayers;       // All enemies must be in a layer
    protected Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if (!isAi && !onCooldown && (keyboard.spaceKey.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame))
        {
            Use();
        }
    }

    override public void Use()
    {
        if (onCooldown)
        {
            return;
        }
        base.Use();
        if (animator != null)
        {
            animator.SetTrigger("Melee");
        }
        // Temporary, flickers white circle showing hitboxes of attacks
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // FIXME: Won't this make it so enemies can kill each other?
        foreach (Collider2D enemy in hitEnemies)
        {
            BaseCharacter enemyChar = enemy.GetComponent<BaseCharacter>();
            if (!enemyChar.invincible)
            {
                enemyChar.TakeDamage(attackDamage);
                StartCoroutine(enemyChar.Knockback(5, 1f, attackPoint.transform));
            }
        }

    }

    // For debugging. Draws circle when in editing mode showing attack range (Must click on ooey to see circle)
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            Debug.Log("Attack point is null");

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
