using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class SummonAbility: MonoBehaviour
{

    GameObject MeleeSlime;
    Vector2[] spawnLocations = new Vector2[4];

    void Start() {
        var addressable = Addressables.LoadAssetAsync<GameObject>("MeleeEnemy");
        addressable.Completed += (obj) => MeleeSlime = obj.Result;
        spawnLocations[0] = new Vector2(-3, -3);
        spawnLocations[1] = new Vector2(3, -3);
        spawnLocations[2] = new Vector2(-3, 3);
        spawnLocations[3] = new Vector2(3, 3);
    }

    public void Use(int stage)
    {
        // If on stage 1, will spawn 2 slimes
        for(int i = 0; i < stage + 1; i++)
        {
            GameObject slime = Instantiate(MeleeSlime, GetComponent<Transform>().position + (Vector3)spawnLocations[i], transform.rotation);
            slime.layer = gameObject.layer;
            slime.GetComponent<Damageable>().initialMaxHealth = 2;
            slime.GetComponent<Damageable>().currentHealth = 2;
        }
    }


}
