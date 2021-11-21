using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class SummonAbility: MonoBehaviour
{

    GameObject MeleeSlime;
    int stage;      // Stage the slime king is in, this determines how many slimes it will spawn
    Vector2[] spawnLocations = new Vector2[4];

    void Start() {
        var addressable = Addressables.LoadAssetAsync<GameObject>("MeleeEnemy");
        addressable.Completed += (obj) => MeleeSlime = obj.Result;
        stage = 1;
        spawnLocations[0] = new Vector2(-1, -1);
        spawnLocations[1] = new Vector2(1, -1);
        spawnLocations[2] = new Vector2(-1, 1);
        spawnLocations[3] = new Vector2(1, 1);
    }

    public void setStage(int _stage)
    {
        stage = _stage;
    }

    public void Use(string key)
    {

        if (key != "Summon")
        {
            return;
        }
        // If on stage 1, will spawn 2 slimes
        for(int i = 0; i < stage + 1; i++)
        {
            GameObject slime = Instantiate(MeleeSlime, GetComponent<Transform>().position + (Vector3)spawnLocations[i], transform.rotation);
            slime.layer = gameObject.layer;
        }
        
    }


}
