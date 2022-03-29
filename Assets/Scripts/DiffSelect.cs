using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffSelect : MonoBehaviour
{
    // Player Section
    public void SetPlayerMaxHP (int val)
    {
      PlayerPrefs.SetInt("PlayerMaxHP", val);
    }

    public void SetPlayerStartHP (int val)
    {
      if (PlayerPrefs.GetInt("PlayerMaxHP") > val)
      {
        PlayerPrefs.SetInt("PlayerStartHP", PlayerPrefs.GetInt("PlayerMaxHP"));
      }
      else
      {
        PlayerPrefs.SetInt("PlayerStartHP", val);
      }
    }

    public void SetPlayerSpeed (float val)
    {
      PlayerPrefs.SetFloat("PlayerSpeed", val);
    }

    // Enemy Section
    public void SetEnemyHPMult (float val)
    {
      PlayerPrefs.SetFloat("EnemyHPMult", val);
    }

    public void SetEnemySpeed (float val)
    {
      PlayerPrefs.SetFloat("EnemySpeed", val);
    }

    public void SetRespawnRate (float val)
    {
      PlayerPrefs.SetFloat("RespawnRate", val);
    }

    // Score Calculator
    // UNDER CONSTRUCTION
    /* Essentially, this section will gather various float values ("multipliers")
       created by the above settings, then combine them into a single "Difficulty Multiplier"
       which will be applied to a player's score at the end of the level.
       We also need to build the player score system.
    */
}
