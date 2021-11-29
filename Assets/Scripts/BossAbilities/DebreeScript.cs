using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebreeScript : MonoBehaviour
{

    SpriteRenderer sr;      // Temporary unitl we have actual sprite/animations
    CircleCollider2D circleCollider;
    Animator animator;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
        animator = GetComponent<Animator>();
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        // Countdown until it hits floor
        yield return new WaitForSecondsRealtime(1.5f);
        animator.SetTrigger("Fall");
    }

    void Fell() // Gets called by last frame of animation
    {
        StartCoroutine(OnGround());
    }

    IEnumerator OnGround()
    {
        circleCollider.enabled = true;
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
