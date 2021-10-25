using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileCollider : MonoBehaviour
{
    public bool isAi;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BaseCharacter enemyChar = collision.gameObject.GetComponent<BaseCharacter>();
        // No need to check enemy layer, missile is spawned on either MissileEnemy or MissilePlayer layer
        // In project settings MissileEnemy layer cannot collide with enemy layer, same with MissilePlayer but with Player layer
        // If the collision is with an enemy, deal damage
        if (enemyChar != null)
        {
            // if the player is the one who shot the missle, knockback the enemy
            if (!isAi)
            {
                enemyChar.Knockback(10f, this.transform);
            }

            enemyChar.TakeDamage(1);
        }
        
        // Flag the missile for deletion
        Destroy(gameObject);
    }
}
