using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MeleeAbility : Ability
{

    public Transform attackPoint;
    public float attackRange = .65f;
    public int attackDamage = 1;


    public override void RequestUse(InputAction.CallbackContext ctx)
    {
        Debug.Log("Player using melee attack");
        if (!onCooldown && animator != null)
        {
            if (animator != null)
            {
                animator.SetTrigger("Melee");
            }
        }
    }

    override public void Use(int key)
    {
        if (key != (int)Ability.Keys.Melee)
            return;

        if (onCooldown)
        {
            return;
        }
        base.Use(key);

        // Temporary, flickers white circle showing hitboxes of attacks
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Swipe");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            BaseCharacter enemyChar = enemy.GetComponent<BaseCharacter>();
            if (!enemyChar.invincible)
            {
                enemyChar.TakeDamage(attackDamage);
                if (!isAi)
                    StartCoroutine(enemyChar.Knockback(2f, GetComponent<Transform>().transform));
            }
        }
        //transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    // For debugging. Draws circle when in editing mode showing attack range (Must click on ooey to see circle)
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            Debug.Log("Attack point is null");

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
