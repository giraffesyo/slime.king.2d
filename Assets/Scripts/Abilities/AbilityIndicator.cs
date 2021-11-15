using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class AbilityIndicator : MonoBehaviour
{
    private bool isSubscribed = false;
    private Ability ability;

    public Image cooldownIndicator;
    public TextMeshProUGUI keybindText;
    public Image keybindImage;

    public void SetAbility(Ability ability)
    {
        this.ability = ability;
        if (!isSubscribed)
        {
            ability.CooldownStarted += StartCooldown;
            isSubscribed = true;
        }
    }

    public void StartCooldown(float cooldown)
    {
        cooldownIndicator.fillAmount = 1f;
        cooldownIndicator.DOFillAmount(0, cooldown).SetEase(Ease.Linear);
    }

    private void OnEnable()
    {
        if (ability != null && !isSubscribed)
        {
            ability.CooldownStarted += StartCooldown;
            isSubscribed = true;
        }
    }

    private void OnDisable()
    {
        if (isSubscribed)
        {
            this.ability.CooldownStarted -= StartCooldown;
            isSubscribed = false;
        }
    }

}
