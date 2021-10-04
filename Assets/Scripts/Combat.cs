using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public LayerMask enemyLayers;       // All enemies must be in a layer
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Do animation here

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            BaseCharacter enemyChar = enemy.GetComponent<BaseCharacter>();
            enemyChar.TakeDamage(attackDamage);
        }
    }

    // Draws circle when in editing mode showing attack range (Must click on ooey to see circle)
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            Debug.Log("Attack point is null");

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
}
