using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class SignUpScreen : MonoBehaviour
{
    public BrowseScreen BrowseScreen;

    void Start()
    {
        BrowseScreen.gameObject.SetActive(true);
        var canvasgroup = GetComponent<CanvasGroup>();
        canvasgroup.DOFade(0, 1f).onComplete += () => gameObject.SetActive(false);
    }
}
