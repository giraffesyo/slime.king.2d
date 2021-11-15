using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AddressableAssets;
public class AbilityBar : MonoBehaviour
{
    List<AbilityIndicator> abilityIndicators;
    float indicatorOffset = 51.45f;
    [SerializeField] private Player player;
    private void Awake()
    {
        // Ability bar has 7 slots
        abilityIndicators = new List<AbilityIndicator>(7);
    }
    private void OnEnable()
    {

        // subscribe to the abilities updated event sent by Player class
        player.AbilitiesUpdated += AbilityListUpdated;
    }

    private void OnDisable()
    {

        // unsubscribe to the abilities updated event sent by Player class
        player.AbilitiesUpdated -= AbilityListUpdated;
    }

    // Update the ability bar with the new list of abilities
    public void AbilityListUpdated(Ability.AbilityKey abilityAdded, int index)
    {
        AddAbilityToBar(abilityAdded, index);
    }

    // Add an ability to the bar by instantiating an ability indicator prefab
    public void AddAbilityToBar(Ability.AbilityKey key, int index)
    {
        // Load the indicator based on the key
        string abilityName = key.ToString();
        var addressable = Addressables.LoadAssetAsync<GameObject>($"Indicators/{abilityName}.prefab");
        // wait for the load to complete
        addressable.Completed += (obj) =>
        {
            // Instantiate the indicator
            AbilityIndicator abilityIndicator = GameObject.Instantiate(obj.Result, transform).GetComponent<AbilityIndicator>();
            if (abilityIndicator != null)
            {
                // Set the indicators keybind text or image
                if (index > 2)
                {
                    abilityIndicator.keybindText.gameObject.active = true;
                    // 2 offset because the first three slots are for engulf, shoot, and slap
                    abilityIndicator.keybindText.text = (index - 2).ToString();
                }
                else
                {
                    // image for the keybind instead of text for left and right mouse
                    if (index == 0)
                    {
                        abilityIndicator.keybindImage.gameObject.active = true;
                    }
                    else if (index == 1)
                    {
                        abilityIndicator.keybindImage.gameObject.active = true;
                    } // E for keybind, different from the rest of abilities
                    else if (index == 2)
                    {
                        abilityIndicator.keybindText.gameObject.active = true;
                        abilityIndicator.keybindText.text = "E";
                    }
                }
                // Add the ability indicator to the list
                abilityIndicators.Add(abilityIndicator);
                // offset the ability indicator by the number of abilities already in the bar
                abilityIndicator.transform.localPosition = new Vector3(indicatorOffset * index, abilityIndicator.transform.localPosition.y, abilityIndicator.transform.localPosition.z);

                // Give the ability indicator a reference to the ability
                abilityIndicator.SetAbility(player.abilities[index]);
            }
        };

    }

}
