using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform spawnPoint;
    bool hasBeenUsed = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasBeenUsed) return;
        if (other.gameObject.CompareTag("Player"))
        {
            hasBeenUsed = true;
            spawnPoint.transform.position = transform.position;
        }
    }
}
