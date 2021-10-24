using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AbilityBar : MonoBehaviour
{
    List<AbilityIndicator> abilityIndicators;
    [SerializeField] private Player player;
    private void OnEnable()
    {
        // subscribe to the abilities updated event sent  by players
        player.AbilitiesUpdated += AbilityListUpdated;
    }

    private void OnDisable()
    {

        // unsubscribe to the abilities updated event sent  by players
        player.AbilitiesUpdated -= AbilityListUpdated;
    }

    public void AbilityListUpdated()
    {
        // first destroy everything in the ability bar
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        // Add each ability back to the bar
        player.abilities.ForEach(ability => AddAbilityToBar(ability.abilityKey));

    }

    public void AddAbilityToBar(Ability.AbilityKey key)
    {
        // Load the indicator based on the key
        string abilityName = key.ToString();
        Debug.Log($"Trying to add Ability {abilityName}");
    }

}
