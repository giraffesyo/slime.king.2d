using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class RangedAbility : Ability
{
    public Transform attackPoint;
    public GameObject missilePrefab;
    public float missileForce;

    Vector2 direction;

    public override void RequestUse(InputAction.CallbackContext ctx, Vector2 aimingDirection)
    {
        if (!onCooldown && animator != null)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(aimingDirection);
            direction = new Vector2(worldPos.x - attackPoint.position.x, worldPos.y - attackPoint.position.y).normalized;
            // not sure if we'll run into this but if we're moving quickly at the time we shoot we could detach from the place the bullet is launched from
            // would need to store "target" location here, and move the calculation into Use if we run into that issue
            animator.SetTrigger("Ranged");
        }
    }
    override public void Use(int key)
    {
        if (key != (int)Ability.Keys.Ranged)
        {
            return;
        }
        Use(direction);
    }

    override public void Use(Vector2 target)
    {
        if (onCooldown)
        {
            return;
        }
        base.Use(target);
        if (target != null)
        {
            Vector2 t = (Vector2)target;
            GameObject missile = Instantiate(missilePrefab, GetComponent<Transform>().position, attackPoint.rotation);
            Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();
            missile.GetComponent<missileColliderEnemy>().isAi = this.isAi;
            rb.AddForce(t * missileForce, ForceMode2D.Impulse);
        }
    }


}
