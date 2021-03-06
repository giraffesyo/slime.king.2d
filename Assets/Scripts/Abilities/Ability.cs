using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public abstract class Ability : MonoBehaviour
{
    public static AudioClip clockSound, levelCompleteSound, levelFailedSound, newLevelSound, praisingUserSound, sadSound, slimeSound, sorrowSound;
    protected SoundManager soundManager;
    static AudioSource audioSrc;

    public enum AbilityKey
    {
        Slap,
        Shoot,
        Engulf,
        Charge,
        Tornado,
        Block,
        Drill,
        Spin
    }
    public AbilityKey abilityKey;
    [SerializeField] protected float cooldown;
    [SerializeField] public bool onCooldown;
    [SerializeField] protected float currentCooldown;

    [SerializeField] public LayerMask enemyLayers;  // All objects must be in a layer

    public delegate void StartCooldownHandler(float duration);
    public delegate void CooldownCompleteHandler();
    public event StartCooldownHandler CooldownStarted;
    public event CooldownCompleteHandler CooldownCompleted;

    public bool isAi;
    protected Animator animator;
    protected BaseCharacter baseCharacter;
    protected SpriteRenderer spriteRenderer;

    public float rotation;

    protected virtual void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        baseCharacter = GetComponent<BaseCharacter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void MasterUse()
    {
        // engulf is handled differently
        if (abilityKey != Ability.AbilityKey.Engulf)
        {
            soundManager.PlayAbilitySound(this.abilityKey);
            onCooldown = true;
            // Play the sound effect for the ability here
            StartCoroutine(StartCooldown());
        }
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

    protected IEnumerator StartCooldown()
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

    // This gets called from an animation event at first frame and last frame
    public void Rotate(int firstFrame)
    {
        if (firstFrame == 0)
        {
            baseCharacter.attacking = true;          // Prevents flipping during ability animation
            if (!baseCharacter.facingRight)
                spriteRenderer.flipX = !spriteRenderer.flipX;       // If facing left, flip to right so rotations make sense
            if ((rotation > 90f || rotation < -90f) && AbilityKey.Engulf != abilityKey)
                spriteRenderer.flipY = !spriteRenderer.flipY;
            transform.Rotate(new Vector3(0, 0, 1), rotation);
        }
        else
        {
            baseCharacter.attacking = false;
        }

        if (firstFrame != 0)
            rotation = 0;
    }
}

