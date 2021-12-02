using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBossMusic : MonoBehaviour
{
    public GameObject musicObject;
    AudioSource source;
    AudioSource bossSource;

    void Start()
    {
        source = musicObject.GetComponent<AudioSource>();
        bossSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        source.enabled = false;
        bossSource.enabled = true;
    }
}
