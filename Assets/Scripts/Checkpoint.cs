using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform spawnPoint;
    bool previouslyActivated = false;
    public SpriteRenderer indicatorSpriteRenderer;
    public Sprite activatedSprite;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (previouslyActivated) return;
        if (other.gameObject.CompareTag("Player"))
        {
            previouslyActivated = true;
            indicatorSpriteRenderer.sprite = activatedSprite;
            spawnPoint.transform.position = transform.position;
        }
    }
}
