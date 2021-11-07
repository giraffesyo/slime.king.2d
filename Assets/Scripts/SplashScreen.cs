using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class SplashScreen : MonoBehaviour
{
    private float secondsToWait = 4.0f;
    [SerializeField] private TextMeshProUGUI SlimeKingText;
    // Start is called before the first frame update
    void Start()
    {
        Sequence sq =  DOTween.Sequence().SetLoops((int)(secondsToWait / 2.0f));
        sq.Append(SlimeKingText.transform.DOScale(new Vector3(1.2f,1.2f,1.0f), 1.0f));
        sq.Append(SlimeKingText.transform.DOScale(new Vector3(1.0f,1.0f,1.0f), 2.0f));

        StartCoroutine(ExitFromSplash());
    }

    private IEnumerator ExitFromSplash(){
        yield return new WaitForSecondsRealtime(secondsToWait);
        SceneManager.LoadSceneAsync("Menu");
    }
}
