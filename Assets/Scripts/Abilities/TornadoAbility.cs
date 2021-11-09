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
        tornadoLayer = LayerMask.NameToLayer("MissileEnemy");

        // Makes sure enemy missiles will not hit enemies
        if (!isAi) { 
            addressable = Addressables.LoadAssetAsync<GameObject>("OoeyTornado");
            tornadoForce = 4f;
            tornadoLayer = LayerMask.NameToLayer("MissilePlayer");
        }
        addressable.Completed += (obj) => tornadoPrefab = obj.Result;
        this.abilityKey = Ability.AbilityKey.Tornado;
        cooldown = 4;
    }

    public override bool RequestUse(InputAction.CallbackContext ctx, Vector2 mousePosition)
    {
        Debug.Log(mousePosition);

        if (!animator)
        {
            Debug.Log("No animator in Tornado Ability");
            return false;
        }
        if (!onCooldown)
        {
            this.targetWorldLocation = mousePosition;
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
        Use(targetWorldLocation - (Vector2)transform.position, targetWorldLocation);
    }

    override public void Use(Vector2 forceDirection, Vector2 targetWorldPosition)
    {
        if (onCooldown)
        {
            return;
        }
        base.Use(forceDirection);
        if (forceDirection != null)
        {
            GameObject tornado = Instantiate(tornadoPrefab, GetComponent<Transform>().position, transform.rotation);
            tornado.layer = tornadoLayer;
            Rigidbody2D rb = tornado.GetComponent<Rigidbody2D>();
            tornado.GetComponent<tornadoCollider>().constructor(true, targetWorldPosition, tornadoPrefab, tornadoForce);
            rb.AddForce(forceDirection.normalized * tornadoForce, ForceMode2D.Impulse);
        }
    }
}
