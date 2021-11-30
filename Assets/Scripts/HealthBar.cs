using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private int xHeartPadding;
    [SerializeField] private float heartScale;
    [SerializeField] private Player player;
    private List<HeartUI> hearts = new List<HeartUI>();

    void OnEnable()
    {
        player.CurrentHealthSet += SetHealth;
        player.MaxHealthSet += SetMaximumHealth;
        player.DamageTaken += TakeDamage;
    }

    void OnDisable()
    {
        player.CurrentHealthSet -= SetHealth;
        player.MaxHealthSet -= SetMaximumHealth;
        player.DamageTaken -= TakeDamage;
    }

    public void TakeDamage(int damage)
    {

        // Find all the filled hearts
        List<HeartUI> filledHearts = hearts.FindAll(heart => heart.filled);
        // put them in reverse order, so we start from the end
        filledHearts.Reverse();
        // determine how much damage to do
        int damageToDo = damage > hearts.Count ? hearts.Count : damage;
        Debug.Log($"{damage} {hearts.Count}");
        // do the damage
        filledHearts.GetRange(0, damageToDo).ForEach(heart => heart.Disable());
    }

    public void SetHealth(int hp)
    {
        Debug.Log($"Setting health to {hp}");
        var count = 0;
        hearts.ForEach(heart =>
        {
            if (count < hp)
            {
                count++;
                heart.Enable();
            }
            else
            {
                heart.Disable();
            }
        });
    }

    public void SetMaximumHealth(int amount)
    {
        // don't do anything if max health is the same
        if (amount == hearts.Count)
        {
            return;
        }
        Debug.Log($"Setting max health to {amount}");
        hearts.ForEach(heart => Destroy(heart.gameObject));
        hearts = new List<HeartUI>();
        for (int i = 0; i < amount; i++)
        {
            // create hearts
            GameObject health = GameObject.Instantiate(heartPrefab, transform);
            // position them next to each other
            health.transform.localPosition = new Vector3(health.transform.localPosition.x + xHeartPadding * i, health.transform.localPosition.y, health.transform.localPosition.z);
            health.transform.localScale = new Vector3(heartScale, heartScale, 1.0f);
            hearts.Add(health.GetComponent<HeartUI>());
        }
    }
}
