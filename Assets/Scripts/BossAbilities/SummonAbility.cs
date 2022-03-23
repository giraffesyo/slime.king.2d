using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SummonAbility : MonoBehaviour
{

    private List<GameObject> summonedEnemies = new List<GameObject>();
    Vector2[] spawnLocations = new Vector2[4];
    public GameObject MeleeEnemyPrefab;
    void Start()
    {
        spawnLocations[0] = new Vector2(-3, -3);
        spawnLocations[1] = new Vector2(3, -3);
        spawnLocations[2] = new Vector2(-3, 3);
        spawnLocations[3] = new Vector2(3, 3);
    }

    public void Use(int stage)
    {


        // foreach (GameObject enemy in summonedEnemies)
        // {
        //     Destroy(enemy);
        // }
        // summonedEnemies.Clear();
        // TODO: Unwind this coroutine, no longer needed.
        StartCoroutine(NextFrame(stage));


    }

    private IEnumerator NextFrame(int stage)
    {
        yield return new WaitForEndOfFrame();
        // If on stage 1, will spawn 2 slimes
        for (int i = 0; i < stage + 1; i++)
        {
            GameObject slime = Instantiate(MeleeEnemyPrefab, transform.position + (Vector3)spawnLocations[i], transform.rotation);
            summonedEnemies.Add(slime);
            slime.layer = gameObject.layer;
            Damageable damageable = slime.GetComponent<Damageable>();
            damageable.initialMaxHealth = 2;
            damageable.currentHealth = 2;

        }
    }

}
