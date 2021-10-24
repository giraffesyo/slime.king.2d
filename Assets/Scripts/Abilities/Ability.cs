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
        Engulf,
        Charge
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

    protected float rotation;
    protected bool locked;    // Band-aid fix? Prevents from melee being spammed in the short window from when animation starts and Use() is called

    protected virtual void Start()
    {
        
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
    public abstract bool RequestUse(InputAction.CallbackContext ctx, Vector2 aimingDirection);

    private IEnumerator StartCooldown()
    {
        locked = false;
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

    // This gets called from an animation event at first frame and last frame
    public void Rotate(int firstFrame)
    {
        BaseCharacter plyr = GetComponent<BaseCharacter>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (firstFrame == 0)
        {
            locked = true;                  // Prevents spamming ability in short window where ability not on cooldown
            plyr.attacking = true;          // Prevents flipping during ability animation
            if (!plyr.facingRight)
                sr.flipX = !sr.flipX;       // If facing left, flip to right so rotations make sense
            if (rotation > 90f || rotation < -90f)
                sr.flipY = !sr.flipY;
            transform.Rotate(new Vector3(0, 0, 1), rotation);
        }
        else
        {
            locked = false;
            plyr.attacking = false;
/*            if (!plyr.facingRight)
                sr.flipX = !sr.flipX;
            rotation = rotation * -1;*/
        }
        if (firstFrame != 0)
            rotation = 0;
    }
}

