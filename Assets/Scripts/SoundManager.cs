using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip clockSound, levelCompleteSound, levelFailedSound, newLevelSound, praisingUserSound, sadSound, slimeSound, sorrowSound;
    static AudioSource audioSrc;

    void Start()
    {
        clockSound = Resources.Load<AudioClip>("clock");
        levelCompleteSound = Resources.Load<AudioClip>("levelComplete");
        levelFailedSound = Resources.Load<AudioClip>("levelFailed");
        newLevelSound = Resources.Load<AudioClip>("newLevel");
        praisingUserSound = Resources.Load<AudioClip>("praisingUser");
        sadSound = Resources.Load<AudioClip>("sad");
        slimeSound = Resources.Load<AudioClip>("slime");
        sorrowSound = Resources.Load<AudioClip>("sorrow");

        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public void PlaySound(string clip)
    {
        switch (clip)
        {
            case "clock":
                audioSrc.PlayOneShot(clockSound);
                break;
            case "levelComplete":
                audioSrc.PlayOneShot(levelCompleteSound);
                break;
            case "levelFailed":
                audioSrc.PlayOneShot(levelFailedSound);
                break;
            case "newLevel":
                audioSrc.PlayOneShot(newLevelSound);
                break;
            case "praisingUser":
                audioSrc.PlayOneShot(praisingUserSound);
                break;
            case "sad":
                audioSrc.PlayOneShot(sadSound);
                break;
            case "slime":
                audioSrc.PlayOneShot(slimeSound);
                break;
            case "sorrow":
                audioSrc.PlayOneShot(sorrowSound);
                break;
        }
    }
}

