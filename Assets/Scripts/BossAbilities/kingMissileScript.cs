using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kingMissileScript : MonoBehaviour
{
    Vector2 targetLocation;
    float explosionRadius;
    LayerMask playerLayer;
    Rigidbody2D rb;

    CircleCollider2D explosionObject;
    SpriteRenderer explosionSR;
    ContactFilter2D enemyFilter;



    // Start is called before the first frame update
    public void Constructor(Vector2 _target)
    {
        targetLocation = _target;
        explosionRadius = 10f;
        playerLayer = LayerMask.GetMask("Player");
        rb = GetComponent<Rigidbody2D>();
        explosionObject = transform.Find("Explosion").GetComponent<CircleCollider2D>();
        explosionSR = transform.Find("Explosion").GetComponent<SpriteRenderer>();

        enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Player"));
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

        explosionSR.enabled = true;
        explosionObject.enabled = true;

        List<Collider2D> hitEnemies = new List<Collider2D>();

        Physics2D.OverlapCollider(explosionObject, enemyFilter, hitEnemies);

        foreach (Collider2D enemy in hitEnemies)
        {
            BaseCharacter enemyChar = enemy.gameObject.GetComponent<BaseCharacter>();

            if (enemyChar != null)
            {
                enemyChar.Knockback(0.3f, this.transform);

                enemyChar.TakeDamage(1);
            }
        }

        StartCoroutine(showExplosion());
    }

    IEnumerator showExplosion()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        Destroy(gameObject);
    }
}
