using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#nullable enable

public class Ability : MonoBehaviour
{
    [SerializeField] protected int cooldown;
    [SerializeField] protected bool onCooldown;
    [SerializeField] protected int currentCooldown;

    public delegate void CooldownCompleteHandler();
    public event CooldownCompleteHandler? CooldownCompleted;

    public virtual void Use()
    {
        onCooldown = true;
        Use(null);
    }
    public virtual void Use(Vector2? target)
    {
        onCooldown = true;
        StartCoroutine(StartCooldown());
    }

    public virtual void Use(Vector2 target, Vector2 source)
    {
        onCooldown = true;
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        currentCooldown = cooldown;

        while (currentCooldown > 0)
        {
            yield return new WaitForSecondsRealtime(1.0f);
            currentCooldown -= 1;
        }
        currentCooldown = 0;
        onCooldown = false;
    }
}

