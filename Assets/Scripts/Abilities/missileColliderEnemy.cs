using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileColliderEnemy : MonoBehaviour
{
    public bool isAi;

    /*private void Start()
    {
        // 10 = enemies, 11 = player, 12 = enemyMissiles, 13 = playerMissiles,
        Physics2D.IgnoreLayerCollision(12, 13);
        Physics2D.IgnoreLayerCollision(12, 10);
        Physics2D.IgnoreLayerCollision(13, 11);
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Flag the missle for deletion
        Destroy(gameObject);

        BaseCharacter enemyChar = collision.gameObject.GetComponent<BaseCharacter>();
        // TODO: Check the enemy layer
        // If the collision is with an enemy, deal damage
        if (enemyChar != null)
        {
            // if the player is the one who shot the missle, knockback the enemy
            if (!isAi)
            {
                enemyChar.Knockback(2f, this.transform);
            }

            enemyChar.TakeDamage(1);
        }
    }
}
