using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kingMissileScript : MonoBehaviour
{
    Vector2 targetLocation;
    float explosionRadius;
    LayerMask playerLayer;
    Rigidbody2D rb;


    // Start is called before the first frame update
    public void Constructor(Vector2 _target)
    {
        targetLocation = _target;
        explosionRadius = 10f;
        playerLayer = LayerMask.GetMask("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, targetLocation) < 0.2f)
        {
            Explode();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BaseCharacter enemyChar = collision.gameObject.GetComponent<BaseCharacter>();

        if (enemyChar != null)
        {
            enemyChar.Knockback(0.3f, this.transform);
           
            enemyChar.TakeDamage(1);
        }


        Explode();
    }

    void Explode()
    {
        rb.velocity = new Vector2(0, 0);
     
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll(transform.position, explosionRadius, playerLayer);
        foreach(Collider2D collider in playerCollider)
        {
            BaseCharacter enemyChar = collider.GetComponent<BaseCharacter>();
            enemyChar.Knockback(0.3f, this.transform);

            enemyChar.TakeDamage(1);
        }

        Destroy(gameObject);
    }
}
