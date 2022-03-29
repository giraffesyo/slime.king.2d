using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Instructions
1. Drop prefab into scene
2. Change size of enemy and spawn arrays (must be same size)
3. Assign enemies and spawn points
4. Hit play!
*/

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnTimer = 30.0f;
    private float spawnClock = 0.0f;
    private bool spawnFlag = true; // Indicates that the enemies need to be spawned

    public GameObject[] enemyArr = new GameObject[0];
    public Vector3[] spawnArr = new Vector3[0];
    // Size of enemyArr and spawnArr can be adjusted in inspector.
    // They *MUST* be the same size, or else you get weird behavior.

    void Start ()
    {
      // Creates the first batch of enemies when the game is started
      for (int i = 0; i < enemyArr.Length; i++)
      {
        Instantiate(enemyArr[i],spawnArr[i],Quaternion.identity,this.transform);
        // Steps through the Enemy Array and spawns each enemy at its
        // corresponding Vector3 indicated in the Spawn Array
      }
      spawnClock = 0.0f;
      spawnFlag = false;
    }

    void FixedUpdate()
    {
      if (spawnFlag)
      {
        if (spawnClock<spawnTimer)
        {
          spawnClock += Time.deltaTime;
        }
        else
        {
          for (int i = 0; i < enemyArr.Length; i++)
          {
            Instantiate(enemyArr[i],spawnArr[i],Quaternion.identity,this.transform);
            // This is the same function as in Start()
            // **Could be converted to a seperate function**
          }
          spawnClock = 0.0f;
          spawnFlag = false;
        }
      }
      else
      {
        if (this.transform.childCount == 0)
        {
          spawnFlag = true;
        }
      }
    }
}

// Edited by Lyle Costo [2/24/2022]
/* NOTES
This could technically be renamed "Spawner", since it just spawns GameObjects
That's up to you, Michael, whether you want to rename it or not.
*/
