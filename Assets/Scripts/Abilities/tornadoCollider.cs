using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tornadoCollider : MonoBehaviour
{
    bool isFirstTornado;
    Vector2 targetLocalLocation;    
    Vector2 targetWorldLocation;
    GameObject tornadoPrefab;
    float tornadoForce;
    bool wallCollisionBlock = true; // Waits a bit until it can collide with walls (otherwise if used while touching wall it will automatically explode)

    Rigidbody2D rb;

    public void constructor(bool _isFirstTornado, Vector2 _targetWorldLocation, GameObject _tornadoPrefab, float _tornadoForce)
    {
        isFirstTornado = _isFirstTornado;
        targetWorldLocation = _targetWorldLocation;
        tornadoPrefab = _tornadoPrefab;
        tornadoForce = _tornadoForce;
        targetLocalLocation = targetLocalLocation - (Vector2)transform.position;

        rb = GetComponent<Rigidbody2D>();

        // If tornado is spawned while touching wall, will not despawn since its not moving towards wall
        StartCoroutine(initialWallCollisionTimer());
        StartCoroutine(startMaxLifetimeCounter());
    }
    
    private void Update()
    {
        //Reached target location
        if(Vector2.Distance(transform.position, targetWorldLocation) < 0.2f || rb.velocity == Vector2.zero)
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (isFirstTornado) { 
            for (int i = 0; i < 8; i++)
            {
                Vector2 direction = new Vector2(Mathf.Cos((45 * i) * Mathf.Deg2Rad) * 2f, Mathf.Sin((45 * i) * Mathf.Deg2Rad) * 2f);
                Vector2 newTargetWorldLocation = new Vector2(targetWorldLocation.x + direction.x, targetWorldLocation.y + direction.y);

                GameObject tornado = Instantiate(tornadoPrefab, transform.position, transform.rotation);
                tornado.layer = gameObject.layer;
                Rigidbody2D rb = tornado.GetComponent<Rigidbody2D>();
                tornado.GetComponent<tornadoCollider>().constructor(false, newTargetWorldLocation, tornadoPrefab, tornadoForce);
                tornado.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                rb.AddForce(direction.normalized * tornadoForce / 4, ForceMode2D.Impulse);
            }
        } 
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BaseCharacter enemyChar = collision.gameObject.GetComponent<BaseCharacter>();
        if (enemyChar != null)
        {
            enemyChar.Knockback(0.8f, this.transform);

            enemyChar.TakeDamage(1);
        }
        else if(!wallCollisionBlock)
            Explode();

        // Flag the missile for deletion
        if (enemyChar != null || !wallCollisionBlock)
            Destroy(gameObject);
    }

    private IEnumerator initialWallCollisionTimer()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        wallCollisionBlock = false;
    }

    private IEnumerator startMaxLifetimeCounter()
    {
        yield return new WaitForSecondsRealtime(5f);
        Destroy(gameObject);
    }

}
