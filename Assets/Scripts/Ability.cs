using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#nullable enable

public class Ability : MonoBehaviour
{
    [SerializeField] protected int cooldown;
    [SerializeField] protected bool onCooldown;
    [SerializeField] protected int currentCooldown;

    [SerializeField] protected LayerMask enemyLayers;  // All enemies must be in a layer

    public delegate void CooldownCompleteHandler();
    public event CooldownCompleteHandler? CooldownCompleted;

    private void MasterUse()
    {
        onCooldown = true;
        StartCoroutine(StartCooldown());
    }

    public virtual void Use()
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

