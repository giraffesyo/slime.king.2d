using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{

    public Sprite icon;
    [SerializeField] protected int cooldown;
    [SerializeField] protected bool onCooldown;
    [SerializeField] protected int currentCooldown;

    [SerializeField] protected LayerMask enemyLayers;  // All enemies must be in a layer

    public delegate void StartCooldownHandler(float duration);
    public delegate void CooldownCompleteHandler();
    public event StartCooldownHandler? CooldownStarted;
    public event CooldownCompleteHandler? CooldownCompleted;

    public bool isAi;
    protected Animator animator;

    private void Start()
    {
        //
        animator = GetComponent<Animator>();
    }

    private void MasterUse()
    {
        onCooldown = true;
        StartCoroutine(StartCooldown());
    }

    // Added key so animation events call the correct Use function (melee calls melee Use() and ranged calls ranged Use())
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

