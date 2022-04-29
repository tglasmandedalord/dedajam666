using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileScreen : MonoBehaviour
{
    [SerializeField] TMP_Text NetWorth;
    [SerializeField] Transform TagContainer;
    [SerializeField] GameObject TagPrefab;
    [SerializeField] Image Dim;
    [SerializeField] RectTransform Container;

    void Awake() {
        Close(true);
    }

    public void Populate() {
        // var netWorth = LoveManager.Inst.MatchedStonkers.Sum(s => int.Parse(s.Networth));

        foreach (Transform child in TagContainer) {
            Destroy(child.gameObject);
        }

        foreach (var tag in LoveManager.Inst.PlayerTags) {
            if (tag.Value > 0) {
                var tagElement = Instantiate(TagPrefab, TagContainer).GetComponent<TagElement>();
                tagElement.Populate(tag);
            }
        }
    }

    public void Open() {
        Populate();

        var canvasGroup = GetComponent<CanvasGroup>();
        Dim.DOFade(1f, 0.5f);
        Container.DOAnchorPosY(0, 0.5f).SetEase(Ease.OutQuad).onComplete += () => {
            canvasGroup.interactable = true;
        };
        canvasGroup.blocksRaycasts = true;
        
    }

    public void Close() => Close(false);
    public void Close(bool instant) {
        var canvasGroup = GetComponent<CanvasGroup>();
        Dim.DOFade(0f, instant ? 0 : 0.5f);
        canvasGroup.interactable = false;
        Container.DOAnchorPosY(-1080, instant ? 0 : 0.5f).SetEase(Ease.InQuad).onComplete += () => {
            canvasGroup.blocksRaycasts = false;
        };
    }
}
