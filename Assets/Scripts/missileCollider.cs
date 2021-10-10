using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileCollider : MonoBehaviour
{
    private void Start()
    {
        // 12 = missile layer, 10 = enemies layer
        Physics2D.IgnoreLayerCollision(12, 10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BaseCharacter c = collision.gameObject.GetComponent<BaseCharacter>();

        if (c != null)
        {
            c.TakeDamage(1);
        }

        Destroy(gameObject);
    }
}
