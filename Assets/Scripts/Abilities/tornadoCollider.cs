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
    bool wallCollisionBlock = true; // Waits a second until it can collide with walls (otherwise if used while touching wall it will automatically explode)

    int c = 0;

    public void constructor(bool _isFirstTornado, Vector2 _targetWorldLocation, GameObject _tornadoPrefab, float _tornadoForce)
    {
        isFirstTornado = _isFirstTornado;
        targetWorldLocation = _targetWorldLocation;
        tornadoPrefab = _tornadoPrefab;
        tornadoForce = _tornadoForce;
        targetLocalLocation = targetLocalLocation - (Vector2)transform.position;

        StartCoroutine(initialWallCollisionTimer());

        Debug.Log(targetWorldLocation);
        Debug.Log(transform.position);
    }
    
    private void Update()
    {
        //Debug.Log(Vector2.Distance(transform.position, targetWorldLocation) + " " + c++);
        //Reached target location
        if(Vector2.Distance(transform.position, targetWorldLocation) < 0.2f)
        {
            if (isFirstTornado)
            {
                for (int i = 0; i < 8; i++)
                {
                    Vector2 direction = new Vector2(Mathf.Cos((45 * i) * Mathf.Deg2Rad) * 2f, Mathf.Sin((45 * i) * Mathf.Deg2Rad) * 2f);
                    Vector2 newTargetWorldLocation = new Vector2(targetWorldLocation.x + direction.x, targetWorldLocation.y + direction.y);

                    GameObject tornado = Instantiate(tornadoPrefab, transform.position, transform.rotation);
                    tornado.layer = gameObject.layer;
                    Rigidbody2D rb = tornado.GetComponent<Rigidbody2D>();
                    tornado.GetComponent<tornadoCollider>().constructor(false, newTargetWorldLocation, tornadoPrefab, tornadoForce);
                    tornado.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    rb.AddForce(direction.normalized * tornadoForce/4, ForceMode2D.Impulse);
                }
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BaseCharacter enemyChar = collision.gameObject.GetComponent<BaseCharacter>();
        if (enemyChar != null)
        {
            enemyChar.Knockback(0.8f, this.transform);

            enemyChar.TakeDamage(1);
        }

        // Flag the missile for deletion
        if (enemyChar != null || !wallCollisionBlock)
            Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("yea");
        BaseCharacter enemyChar = collision.gameObject.GetComponent<BaseCharacter>();
        if (enemyChar != null)
        {
            enemyChar.Knockback(0.8f, this.transform);

            enemyChar.TakeDamage(1);
        }

        // Flag the missile for deletion
        if (enemyChar != null || !wallCollisionBlock)
            Destroy(gameObject);
    }

    private IEnumerator initialWallCollisionTimer()
    {
        yield return new WaitForSeconds(1f);
        wallCollisionBlock = false;
    }
}
