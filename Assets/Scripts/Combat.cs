using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Combat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public bool isAi;

    public LayerMask enemyLayers;       // All enemies must be in a layer

    int attackCooldown = 50; //Temporary variables
    int cooldownCounter = 0;

    private void FixedUpdate()
    {
        if (cooldownCounter != 0)
        {
            cooldownCounter = (cooldownCounter + 1) % attackCooldown;   // fixedUpdate gets called 50 times per second
            if (cooldownCounter == 0)
            {
                transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if (!isAi && keyboard.spaceKey.wasPressedThisFrame)
        {
            Attack();
        }
    }

    public void Attack()
    {
        // Do animation here

        // Temporary, flickers white circle showing hitboxes of attacks
        cooldownCounter = 1;
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            BaseCharacter enemyChar = enemy.GetComponent<BaseCharacter>();
            enemyChar.TakeDamage(attackDamage);
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
