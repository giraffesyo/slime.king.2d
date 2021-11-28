using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    /*
    How To Use This Script
    1. Place this script on a Game Object with a 2D Collider (this object will be your "button")
    2. Create a Door as its own Tilemap Game Object (add colliders like a wall)
    3. Drag the Door object to this script's Door field
    4. Set desired time limit (how long the player must stand on the "button" to open the door)
    5. You're done!
    */


    [SerializeField] private bool flagOpen;
    [SerializeField] private double timer = 0.0;
    [SerializeField] private double timeLimit = 2.0;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private BoxCollider2D doorCollider;
    void FixedUpdate()
    {
        if (flagOpen)
        {
            timer += Time.deltaTime;
            if (timer >= timeLimit)
            {
                // allow walking through the door now
                doorCollider.enabled = false;
                flagOpen = false;
                doorAnimator.SetTrigger("open");
                // disable this script, because we no longer need to check if they are touching the button
                // this.enabled = false;
            }
        }
        else
        {
            timer = 0.0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            flagOpen = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (timer < timeLimit)
            {
                flagOpen = false;
            }
        }
    }
}
