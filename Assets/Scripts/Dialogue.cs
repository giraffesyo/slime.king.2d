using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dialogue : MonoBehaviour
{
    public GameObject dialogue = null;
    // Start is called before the first frame update
    void Start()
    {
        dialogue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialogue.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogue.SetActive(false);
    }
}
