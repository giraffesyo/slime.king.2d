using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AddressableAssets;

public class TornadoAbility : Ability
{
#pragma warning disable CS8618
    private GameObject tornadoPrefab;
    private float tornadoForce = 3f;
    Vector2 targetWorldLocation;
    int tornadoLayer;

#pragma warning restore CS8618

    override protected void Start()
    {
        base.Start();
        var addressable = Addressables.LoadAssetAsync<GameObject>("tornado");
        addressable.Completed += (obj) => tornadoPrefab = obj.Result;
        this.abilityKey = Ability.AbilityKey.Tornado;

        // Makes sure enemy missiles will not hit enemies
        if (isAi)
            tornadoLayer = LayerMask.NameToLayer("MissileEnemy");
        else
            tornadoLayer = LayerMask.NameToLayer("MissilePlayer");

    }

    public override bool RequestUse(InputAction.CallbackContext ctx, Vector2 targetWorldLocation)
    {
        if (!animator)
        {
            Debug.Log("No animator in Tornado Ability");
            return false;
        }
        if (!onCooldown && !locked)
        {
            this.targetWorldLocation = targetWorldLocation;
            rotation = Mathf.Atan2(this.targetWorldLocation.y, this.targetWorldLocation.x) * Mathf.Rad2Deg;
            animator.SetTrigger("Tornado");
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
        Use(targetWorldLocation - (Vector2)transform.position);
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
            GameObject tornado = Instantiate(tornadoPrefab, GetComponent<Transform>().position, transform.rotation);
            tornado.layer = tornadoLayer;
            Rigidbody2D rb = tornado.GetComponent<Rigidbody2D>();
            tornado.GetComponent<tornadoCollider>().constructor(true, targetWorldLocation, tornadoPrefab, tornadoForce);
            rb.AddForce(target.normalized * tornadoForce, ForceMode2D.Impulse);
        }
    }
}
