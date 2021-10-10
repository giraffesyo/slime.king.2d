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
        filled = true;
    }
    public void FillHeart()
    {
        // could potentially animate here, but right now we're just swapping between images
        image.sprite = fullHeartSprite;
        filled = true;
    }

    public void EmptyHeart()
    {
        image.sprite = emptyHeartSprite;
        filled = false;
    }

}
