using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private bool flagOpen;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private BoxCollider2D doorCollider;
    [SerializeField] private GameObject pressedButton;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // allow walking through the door now
            doorCollider.enabled = false;
            spriteRenderer.enabled = false;
            pressedButton.SetActive(true);
            flagOpen = false;
            doorAnimator.SetTrigger("open");

        }
    }
}
