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

    void Awake()
    {
        player.DamageTaken += TakeDamage;
        player.MaxHealthIncrased += IncreaseMaximumHealth;
        player.MaxHealthSet += SetMaximumHealth;
        player.HealthRestored += RestoreHealth;
    }

    public void TakeDamage(int amount = 1)
    {

        for (int i = 0; i < amount; i++)
        {
            // Find last filled heart, unfill it
            HeartUI lastFilled = hearts.FindLast(heart => heart.filled);
            if (lastFilled != null)
            {
                lastFilled.EmptyHeart();
            }

        }

    }


    public void RestoreHealth(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            // Find first empty heart, fill it
            HeartUI firstEmpty = hearts.Find(heart => !heart.filled);
            if (firstEmpty != null)
            {
                firstEmpty.FillHeart();
            }

        }
    }

    public void IncreaseMaximumHealth(int amount = 1)
    {
        SetMaximumHealth(hearts.Count + amount);
    }

    public void SetMaximumHealth(int amount)
    {
        Debug.Log($"Setting max health to {amount}");
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
