using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    // Max Health can never exceed absolute max
    [SerializeField] protected int absoluteMaxHealth = 5;
    // Max health beings at this number
    [SerializeField] public int initialMaxHealth = 1;
    // The current value of max health, can be increased up to absolute max
    protected int _maxHealth;
    public int maxHealth
    {
        get
        {
            return _maxHealth;
        }
    }
    // Current amount of HP available
    public int currentHealth;
    public bool atMaxHealth
    {
        get
        {
            return currentHealth == _maxHealth;
        }
    }

    public bool isInvincible = false;

    public delegate void TakeDamageHandler(int amount);
    public delegate void RestoreHealthHanlder(int amount);
    public delegate void SetMaxHealthHandler(int amount);
    public delegate void SetCurrentHealthHandler(int amount);
    public event TakeDamageHandler DamageTaken;
    public event RestoreHealthHanlder HealthRestored;
    public event SetMaxHealthHandler MaxHealthSet;
    public event SetCurrentHealthHandler CurrentHealthSet;

    protected Color originalColor;

    // Pass in negative damage to restore health
    public virtual void TakeDamage(int damage)
    {
        if (isInvincible)
            return;

        if (damage < 0)
        {
            if (HealthRestored != null)
            {
                HealthRestored.Invoke(damage);
            }
        }
        else
        {
            if (DamageTaken != null)
            {
                DamageTaken.Invoke(damage);
            }
        }
        // Do hurt animation ?
        StartCoroutine(dmgAnimation());
        currentHealth -= damage;
        Debug.Log($"{transform.name} took {damage} points of damage, new health is {currentHealth}");
    }

    public virtual void SetMaxHealth(int health)
    {
        currentHealth = health;
        _maxHealth = health;
        // Don't allow to exceed the absolute max
        if (health >= absoluteMaxHealth)
        {
            currentHealth = absoluteMaxHealth;
            _maxHealth = absoluteMaxHealth;
        }
        
        if (MaxHealthSet != null)
             MaxHealthSet.Invoke(currentHealth);
    }
    public virtual void SetCurrentHealth(int health)
    {
        if (currentHealth <= _maxHealth)
        {
            currentHealth = health;
        }
        else
        {
            currentHealth = _maxHealth;
        }
        if (CurrentHealthSet != null)
        {
            CurrentHealthSet.Invoke(currentHealth);
        }

    }

    public IEnumerator dmgAnimation()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(255, 0, 0, 255);

        yield return new WaitForSecondsRealtime(.1f);
        sr.color = originalColor;

    }

    public IEnumerator ActivateInvincibility(float forSeconds)
    {
        isInvincible = true;
        yield return new WaitForSecondsRealtime(forSeconds);
        isInvincible = false;
    }
}
