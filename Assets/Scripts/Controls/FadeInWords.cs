using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class FadeInWords : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        float amount = 0;
        float decreaseBy = 255 / 30;

        for(int i =0; i < 30; i++)
        {
            amount += decreaseBy;
            text.color = new Color(amount/255, amount/255, amount/255, amount/255);
            yield return new WaitForSecondsRealtime(0.1f);
        }

        yield return new WaitForSecondsRealtime(1f);


        amount = 255;

        for (int i = 0; i < 30; i++)
        {
            amount -= decreaseBy;
            text.color = new Color(amount / 255, amount / 255, amount / 255, amount / 255);
            yield return new WaitForSecondsRealtime(0.1f);
        }

        SceneManager.LoadScene("Creditsv2", LoadSceneMode.Single);
    }
}
