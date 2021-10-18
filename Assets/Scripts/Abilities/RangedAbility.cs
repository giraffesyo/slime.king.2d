using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class RangedAbility : Ability
{

    public GameObject missilePrefab;
    public float missileForce;

    Vector2 aimingDirection;


    public override void RequestUse(InputAction.CallbackContext ctx, Vector2 aimingDirection)
    {
        if (!animator)
        {
            Debug.Log("No animator in Ranged Ability");
            return;
        }
        if (!onCooldown && !locked)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(aimingDirection);
            this.aimingDirection = new Vector2(worldPos.x - transform.position.x, worldPos.y - transform.position.y).normalized;
            rotation = Mathf.Atan2(this.aimingDirection.y, this.aimingDirection.x) * Mathf.Rad2Deg;
            // not sure if we'll run into this but if we're moving quickly at the time we shoot we could detach from the place the bullet is launched from
            // would need to store "target" location here, and move the calculation into Use if we run into that issue
            animator.SetTrigger("Ranged");
        }
    }
    override public void Use(int key)
    {
        if (key != (int)Ability.BasicAbilityKeys.Ranged)
        {
            return;
        }
        Use(aimingDirection);
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
            GameObject missile = Instantiate(missilePrefab, GetComponent<Transform>().position, transform.rotation);
            Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();
            missile.GetComponent<missileColliderEnemy>().isAi = this.isAi;
            rb.AddForce(t * missileForce, ForceMode2D.Impulse);
        }
    }


}
