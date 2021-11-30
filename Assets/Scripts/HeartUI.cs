using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;
    public bool filled;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = fullHeartSprite;
        image.enabled = true;
        filled = true;
    }
    public void Enable()
    {
        image.enabled = true;
        filled = true;
    }

    public void Disable()
    {
        image.enabled = false;
        filled = false;
    }

}
