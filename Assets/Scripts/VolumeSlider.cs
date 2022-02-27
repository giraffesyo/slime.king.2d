using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class VolumeSlider : MonoBehaviour {

    // Reference to Audio Source component
    private AudioSource audioSrc;
    public Slider slider;
    // Music volume variable that will be modified
    // by dragging slider knob
    private float musicVolume = 1f;

	// Use this for initialization
	void Start () {

        // Assign Audio Source component to control it
        audioSrc = GetComponent<AudioSource>();
        
	}
	
	// Update is called once per frame
	void Update () {

           slider.value = PlayerPrefs.GetFloat("volume", musicVolume);
        audioSrc.volume = PlayerPrefs.GetFloat("volume", musicVolume);
	}

    public void SetVolume(float vol)
    {
        musicVolume = vol;
        PlayerPrefs.SetFloat("volume", musicVolume);   
    }
}
