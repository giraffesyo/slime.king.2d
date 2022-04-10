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
    [SerializeField] private float spawnTimer;
    private float spawnClock;
    private bool spawnFlag;

    public GameObject[] enemyArr = new GameObject[0];
    public Vector3[] spawnArr = new Vector3[0];
    // Size of enemyArr and spawnArr can be adjusted in inspector.
    // They *MUST* be the same size, or else you get weird behavior.

    void Start ()
    {
      spawnTimer = PlayerPrefs.GetFloat("RespawnRate", 30.0f);
      SpawnOAP(enemyArr,spawnArr);
      spawnClock = 0.0f;
      spawnFlag = false;
    }

    void FixedUpdate()
    {
      if (spawnFlag && spawnTimer != -1)
      {
        if (spawnClock < spawnTimer)
        {
          spawnClock += Time.deltaTime;
        }
        else
        {
          SpawnOAP(enemyArr,spawnArr);
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

    private void SpawnOAP(GameObject[] objectArr, Vector3[] pointArr)
    {
      int maxSpawn = objectArr.Length;
      for (int i = 0; i < maxSpawn; i++)
      {
        if (i < pointArr.Length)
        {
          Instantiate(enemyArr[i],spawnArr[i],Quaternion.identity,this.transform);
        }
        else
        {
          Instantiate(enemyArr[i],Vector3.zero,Quaternion.identity,this.transform);
        }
      }
    }
}

// Edited by Lyle Costo [2/24/2022]
/* NOTES
This could technically be renamed "Spawner", since it just spawns GameObjects
That's up to you, Michael, whether you want to rename it or not.
*/
