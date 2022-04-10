using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffSelect : MonoBehaviour
{
    private int PlayerMaxHP;
    private int PlayerStartHP;
    private float PlayerSpeed;
    private float EnemyHPMult;
    private float EnemySpeed;
    private float RespawnRate;
    private GameObject toggle;

    void Awake()
    {
      PlayerMaxHP = PlayerPrefs.GetInt("PlayerMaxHP", 5);
      PlayerStartHP = PlayerPrefs.GetInt("PlayerStartHP", 3);
      PlayerSpeed = PlayerPrefs.GetFloat("PlayerSpeed", 1.0f);
      EnemyHPMult = PlayerPrefs.GetFloat("EnemyHPMult", 1.0f);
      EnemySpeed = PlayerPrefs.GetFloat("EnemySpeed", 1.0f);
      RespawnRate = PlayerPrefs.GetFloat("RespawnRate", 30.0f);
    }

    public void SaveSettings ()
    {
      PlayerPrefs.SetInt("PlayerMaxHP", PlayerMaxHP);
      PlayerPrefs.SetInt("PlayerStartHP", PlayerStartHP);
      PlayerPrefs.SetFloat("PlayerSpeed", PlayerSpeed);
      PlayerPrefs.SetFloat("EnemyHPMult", EnemyHPMult);
      PlayerPrefs.SetFloat("EnemySpeed", EnemySpeed);
      PlayerPrefs.SetFloat("RespawnRate", RespawnRate);
      PlayerPrefs.Save();
    }

    // Player Section
    public void SetPlayerMaxHP (int val)
    {
      PlayerMaxHP = val;
    }

    public void SetPlayerStartHP (int val)
    {
      if (PlayerStartHP < PlayerMaxHP)
      {
        PlayerStartHP = val;
      }
      else
      {
        PlayerStartHP = PlayerMaxHP;
      }
    }

    public void SetPlayerSpeed (float val)
    {
      PlayerSpeed = val;
    }

    // Enemy Section
    public void SetEnemyHPMult (float val)
    {
      EnemyHPMult = val;
    }

    public void SetEnemySpeed (float val)
    {
      EnemySpeed = val;
    }

    public void SetRespawnRate (float val)
    {
      RespawnRate = val;
    }


    // Score Calculator
    // UNDER CONSTRUCTION
    /* Essentially, this section will gather various float values ("multipliers")
       created by the above settings, then combine them into a single "Difficulty Multiplier"
       which will be applied to a player's score at the end of the level.
       We also need to build the player score system.
    */
}
