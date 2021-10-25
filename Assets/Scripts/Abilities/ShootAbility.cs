using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AddressableAssets;
#nullable enable
public class ShootAbility : Ability
{
#pragma warning disable CS8618
    private GameObject missilePrefab;
    private float missileForce = 10f;
    Vector2 aimingDirection;

#pragma warning restore CS8618

    override protected void Start()
    {
        base.Start();
        var addressable = Addressables.LoadAssetAsync<GameObject>("missle");
        addressable.Completed += (obj) => missilePrefab = obj.Result;
        this.abilityKey = Ability.AbilityKey.Shoot;
    }

    public override bool RequestUse(InputAction.CallbackContext ctx, Vector2 aimingDirection)
    {
        if (!animator)
        {
            Debug.Log("No animator in Ranged Ability");
            return false;
        }
        if (!onCooldown && !locked)
        {
            this.aimingDirection = aimingDirection;
            rotation = Mathf.Atan2(this.aimingDirection.y, this.aimingDirection.x) * Mathf.Rad2Deg;
            // not sure if we'll run into this but if we're moving quickly at the time we shoot we could detach from the place the bullet is launched from
            // would need to store "target" location here, and move the calculation into Use if we run into that issue
            animator.SetTrigger("Ranged");
            return true;
        }
        return false;
    }
    override public void Use(int key)
    {
        if (key != (int)this.abilityKey)
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
