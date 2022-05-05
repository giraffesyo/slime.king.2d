using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSlap : MonoBehaviour
{
    public GameObject melee = null;
    private GameObject slapUI = null;
    private bool slapOn = true;

    // Start is called before the first frame update
    void Start()
    {
        slapOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (slapOn)
        {
            slapUI = GameObject.Find("Slap(Clone)");
            slapUI.SetActive(false);
            slapOn = false;
        }


        if (melee == null)
        {
            slapUI.SetActive(true);
            this.enabled = false;
        }

    }
}
