using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShoot : MonoBehaviour
{
    public GameObject ranged = null;
    private GameObject shootUI = null;
    private bool shootOn = true;

    // Start is called before the first frame update
    void Start()
    {
        shootOn = true;        
    }

    // Update is called once per frame
    void Update()
    {
        if (shootOn)
        {
            shootUI = GameObject.Find("Shoot(Clone)");
            shootUI.SetActive(false);
            shootOn = false;
        }
        
        
        if (ranged == null)
        {
            shootUI.SetActive(true);
            this.enabled = false;
        }
        
    }
}
