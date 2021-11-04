using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class EngulfAbility : Ability
{
    private PolygonCollider2D engulfCollider;
    private ContactFilter2D enemyFilter;
    private Player player;
    override protected void Start()
    {
        base.Start();
        abilityKey = Ability.AbilityKey.Engulf;
        enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(enemyLayers);
        player = GetComponent<Player>();
    }
    public override bool RequestUse(InputAction.CallbackContext ctx, Vector2 aimingDirection)
    {
        Debug.Log("Engulf requested");
        if (animator == null)
        {
            Debug.Log("No animator on Engulf");
            return false;
        }

        if (!onCooldown)
        {
            rotation = Mathf.Atan2(aimingDirection.y, aimingDirection.x) * Mathf.Rad2Deg - 90;
            animator.SetTrigger("Engulf");
            return true;
        }
        return false;
    }

    override public void Use(int key)
    {
        if (key != (int)this.abilityKey)
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

            AI enemyChar = enemy.GetComponent<AI>();
            if (enemyChar && enemyChar.engulfable) // && enemy is not the boss
            {
                // grab a copy of their ability key
                Ability enemyAbility = enemyChar.GetComponent<Ability>();
                Ability.AbilityKey abilityKey = enemyAbility.abilityKey;
                // Tell the player script we should obtain this ability
                player.ObtainAbility(abilityKey);
                // kill the enemy
                enemyChar.Die();
                if (player.atMaxHealth)
                {
                    // increase their max health
                    player.SetMaxHealth(player.maxHealth + 1);
                }
                else
                {
                    player.SetCurrentHealth(player.currentHealth + 1);
                }

            }
        }
    }
}
