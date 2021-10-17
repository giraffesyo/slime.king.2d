using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public abstract class Ability : MonoBehaviour
{

    public enum BasicAbilityKeys
    {
        Melee,
        Ranged,
        Engulf
    }
    
    [SerializeField] protected int cooldown;
    [SerializeField] protected bool onCooldown;
    [SerializeField] protected int currentCooldown;

    [SerializeField] protected LayerMask enemyLayers;  // All enemies must be in a layer

    public delegate void StartCooldownHandler(float duration);
    public delegate void CooldownCompleteHandler();
    public event StartCooldownHandler CooldownStarted;
    public event CooldownCompleteHandler CooldownCompleted;

    public bool isAi;
    protected Animator animator;

    protected virtual void Start()
    {
        //
        animator = GetComponent<Animator>();
    }

    private void MasterUse()
    {
        onCooldown = true;
        StartCoroutine(StartCooldown());
    }

    // Animation events set in Animation Clip Editor call Use with their key, then we return if its not supposed to be handled by us.
    // i.e. melee calls melee Use() and ranged calls ranged Use()
    // This is an effect of having multiple Use functions on a single game object
    public virtual void Use(int key)
    {
        MasterUse();
    }
    public virtual void Use(Vector2 target)
    {
        MasterUse();
    }

    public virtual void Use(BaseCharacter target)
    {
        MasterUse();
    }
    public virtual void Use(Vector2 target, Vector2 source)
    {
        MasterUse();
    }

    // Player input triggers this function to be called
    public abstract void RequestUse(InputAction.CallbackContext ctx, Vector2 aimingDirection);

    private IEnumerator StartCooldown()
    {
        currentCooldown = cooldown;
        if (CooldownStarted != null)
        {
            CooldownStarted.Invoke(cooldown);
        }
        while (currentCooldown > 0)
        {
            yield return new WaitForSecondsRealtime(1.0f);
            currentCooldown -= 1;
        }
        currentCooldown = 0;
        onCooldown = false;
        if (CooldownCompleted != null)
        {
            CooldownCompleted.Invoke();
        }
    }
}

