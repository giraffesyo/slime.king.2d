using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AddressableAssets;

public class BlockAbility : Ability
{
#pragma warning disable CS8618
    private SpriteRenderer blockObjectSR;     // Handles if block sprite is visible

#pragma warning restore CS8618

    override protected void Start()
    {
        base.Start();
        abilityKey = AbilityKey.Block;
        blockObjectSR = transform.Find("BlockObject").GetComponent<SpriteRenderer>();
        cooldown = 6;
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
            animator.SetTrigger("Block");
            return true;
        }
        return false;
    }
    override public void Use(int key)
    {
        if (key != (int)this.abilityKey || onCooldown)
        {
            return;
        }
        base.Use(key);

        animator.SetBool("isBlocking", true);
        blockObjectSR.enabled = true;
        StartCoroutine(doBlock(3f));
    }

    private IEnumerator doBlock(float seconds)
    {
        yield return StartCoroutine(baseCharacter.ActivateInvincibility(2f));
        animator.SetBool("isBlocking", false);
        blockObjectSR.enabled = false;
    }
}
