using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] protected int initialMaxHealth = 3;
    protected int maxHealth;
    [SerializeField] protected int currentHealth;
    public delegate void TakeDamageHandler(int amount);
    public delegate void RestoreHealthHanlder(int amount);
    public delegate void SetMaxHealthHandler(int amount);
    public delegate void SetCurrentHealthHandler(int amount);
    public event TakeDamageHandler DamageTaken;
    public event RestoreHealthHanlder HealthRestored;
    public event SetMaxHealthHandler MaxHealthSet;
    public event SetCurrentHealthHandler CurrentHealthSet;


    void Start()
    {
        SetMaxHealth(initialMaxHealth);
    }


    // Pass in negative damage to restore health
    public virtual void TakeDamage(int damage)
    {
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
        currentHealth -= damage;
        Debug.Log($"{transform.name} took {damage} points of damage, new health is {currentHealth}");
    }

    public virtual void SetMaxHealth(int health)
    {
        currentHealth = health;
        maxHealth = health;
        if (MaxHealthSet != null)
        {
            MaxHealthSet.Invoke(health);
        }

    }
    public virtual void SetCurrentHealth(int health)
    {
        if (currentHealth <= maxHealth)
        {

            currentHealth = health;
        }
        else
        {
            currentHealth = maxHealth;
        }
        if (CurrentHealthSet != null)
        {
            CurrentHealthSet.Invoke(currentHealth);
        }

    }
}
