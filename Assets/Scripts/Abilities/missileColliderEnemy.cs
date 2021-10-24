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
        BaseCharacter c = collision.gameObject.GetComponent<BaseCharacter>();

        if (c != null)
        {            
            if(!isAi)
                c.Knockback(2f, this.transform);
            c.TakeDamage(1);
        }
        Destroy(gameObject);
    }
}
