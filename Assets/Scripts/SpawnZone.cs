using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float spawnRate = 1f;
    public float lastSpawnTime = 0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Time.time >= lastSpawnTime + spawnRate)
            {
                lastSpawnTime = Time.time;
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
