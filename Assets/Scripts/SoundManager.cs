using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class SoundManager : MonoBehaviour
{
    private AudioClip shoot;
    private AudioClip slap;
    private AudioClip charge;
    private AudioClip engulf;
    private AudioClip block;
    private AudioSource audioSource;

    void Start()
    {
        LoadSounds();
        audioSource = GetComponent<AudioSource>();
    }

    public void LoadSounds()
    {
        var shootSound = Addressables.LoadAssetAsync<AudioClip>("sounds/shoot.wav");
        shootSound.Completed += (obj) =>
        {
            shoot = obj.Result;
        };
        var slapSound = Addressables.LoadAssetAsync<AudioClip>("sounds/slap.wav");
        slapSound.Completed += (obj) =>
        {
            slap = obj.Result;
        };
        var chargeSound = Addressables.LoadAssetAsync<AudioClip>("sounds/charge.wav");
        chargeSound.Completed += (obj) =>
        {
            charge = obj.Result;
        };
        var engulfSound = Addressables.LoadAssetAsync<AudioClip>("sounds/engulf.wav");
        engulfSound.Completed += (obj) =>
        {
            engulf = obj.Result;
        };
        var blockSound = Addressables.LoadAssetAsync<AudioClip>("sounds/block.wav");
        blockSound.Completed += (obj) =>
        {
            block = obj.Result;
        };

    }

    public void PlayAbilitySound(Ability.AbilityKey ability)
    {
        switch (ability)
        {
            case Ability.AbilityKey.Shoot:
                audioSource.PlayOneShot(shoot);
                break;
            case Ability.AbilityKey.Slap:
                audioSource.PlayOneShot(slap);
                break;
            case Ability.AbilityKey.Engulf:
                audioSource.PlayOneShot(engulf);
                break;
            case Ability.AbilityKey.Block:
                audioSource.PlayOneShot(block);
                break;
            case Ability.AbilityKey.Charge:
                audioSource.PlayOneShot(charge);
                break;
        }

    }

}

