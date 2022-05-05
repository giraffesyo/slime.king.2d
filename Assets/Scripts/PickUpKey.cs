using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    public GameObject key = null;
    // Start is called before the first frame update
    void Start()
    {
        key.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        key.SetActive(false);
    }

}
