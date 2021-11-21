using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class DebreeAttack : MonoBehaviour
{
    GameObject debree;
    // 4 corners of boss room, currently set to 4 corners of Eric Scene
    float minX = -8.35f;
    float maxX = 25f;
    float minY = -9.5f;
    float maxY = 1.5f;


    void Start()
    {
        var addressable = Addressables.LoadAssetAsync<GameObject>("Debree");
        addressable.Completed += (obj) => debree = obj.Result;

    }
    public void Use(string key)
    {
        if (key != "Debree")
        {
            return;
        }
        // Better system, split space into quadrants and do random values within quadrants that way its 
        // more guaranteed its uniformly distrubuted with a small sample
        for(int i = 0; i < 30; i++)
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);

            GameObject debreeObject = Instantiate(debree, new Vector3(x, y, 1), transform.rotation);
        }
    }
}
