using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class RangedAttack : MonoBehaviour
{
    GameObject missile;
    Transform playerTransform;
    Rigidbody2D playerRb;
    float force = 3f;

    void Start()
    {
        var addressable = Addressables.LoadAssetAsync<GameObject>("kingMissile");
        addressable.Completed += (obj) => missile = obj.Result;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObj.transform;
        playerRb = playerObj.GetComponent<Rigidbody2D>();

    }
    public void Use(string key)
    {
        if (key != "Ranged")
        {
            return;
        }

        // Will shoot missile a bit ahead of the player
        Vector3 playerPositionPrediction = new Vector3(playerTransform.position.x + playerRb.velocity.x/2, playerTransform.position.y + playerRb.velocity.y/2, 1);

        Vector2 direction = (playerPositionPrediction - transform.position).normalized;

        GameObject missileObject = Instantiate(missile, transform.position, transform.rotation);
        Rigidbody2D rb = missileObject.GetComponent<Rigidbody2D>();
        missileObject.GetComponent<kingMissileScript>().Constructor(playerPositionPrediction);
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
}
