using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebreeScript : MonoBehaviour
{

    SpriteRenderer sr;      // Temporary unitl we have actual sprite/animations
    CircleCollider2D collider;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
        collider.enabled = false;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        // Countdown until it hits floor, sprite is black circle
        sr.color = new Color(0, 0, 0);
        yield return new WaitForSecondsRealtime(1.5f);
        collider.enabled = true;
        sr.color = new Color(255, 255, 255);
        // Change sprite to actual debree
        // Worry about animations later?
        // Countdown until it dissapears
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BaseCharacter enemyChar = collision.gameObject.GetComponent<BaseCharacter>();
        if (enemyChar != null)
        {
           enemyChar.Knockback(0.5f, this.transform);

           enemyChar.TakeDamage(1);
        }

    }
}
