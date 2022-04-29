using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class SplashScreen : MonoBehaviour
{
    public GameObject SignUpScreen;

    void Start()
    {
        StartCoroutine(Splash());
    }

    IEnumerator Splash()
    {
        yield return new WaitForSeconds(2f);
        var canvasgroup = GetComponent<CanvasGroup>();
        canvasgroup.DOFade(0, 1f).onComplete += () => gameObject.SetActive(false);
    }
}
