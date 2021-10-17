using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class EngulfAbility : Ability
{
    private PolygonCollider2D engulfCollider;
    private ContactFilter2D enemyFilter;
    override protected void Start()
    {
        base.Start();
        enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(enemyLayers);
    }
    public override void RequestUse(InputAction.CallbackContext ctx, Vector2 aimingDirection)
    {
        Debug.Log("Engulf requested");
        if (animator == null)
        {
            Debug.Log("No animator on Engulf");
            return;
        }

        if (!onCooldown)
        {
            animator.SetTrigger("Engulf");
        }
    }

    override public void Use(int key)
    {
        if (key != (int)Ability.BasicAbilityKeys.Engulf)
            return;
        if (onCooldown)
        {
            return;
        }
        base.Use(key);

        if (engulfCollider == null)
        {
            engulfCollider = gameObject.AddComponent<PolygonCollider2D>();
            engulfCollider.isTrigger = true;

        }
        StartCoroutine(NextFrame());
    }

    private IEnumerator NextFrame()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        List<Collider2D> hitEnemies = new List<Collider2D>();

        Physics2D.OverlapCollider(engulfCollider, enemyFilter, hitEnemies);


        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log($"Trying to engulf {enemy.name}");
            AI enemyChar = enemy.GetComponent<AI>();
            if (enemyChar != null)
            {
                Debug.Log($"Trying to engulf {enemyChar.name}");
            }

        }
    }
}
