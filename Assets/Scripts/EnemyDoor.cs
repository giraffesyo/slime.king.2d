using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* EnemyDoor prefab should have this script attached as well as the door attached to the doorAnimator and doorCollider variables.
 * You enter the number of enemies next to "enemies", then drag each enemy from the hierarchy to each slot created. */ 

public class EnemyDoor : MonoBehaviour
{
    public GameObject[] enemies;
    public Animator doorAnimator;
    public BoxCollider2D doorCollider;
    private bool enemiesAlive;

    // Start is called before the first frame update
    void Start()
    {
        enemiesAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)  //enemy is still active
            {
                enemiesAlive = true;
                break;
            }
            else
                enemiesAlive = false;
        }
        if (!enemiesAlive)
        {
            doorAnimator.SetTrigger("open");
            doorCollider.enabled = false;
            this.enabled = false;
        }
    }
}

