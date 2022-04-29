using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class MatchPopup : MonoBehaviour
{
    [SerializeField] Image UserProfilePic;
    [SerializeField] TMP_Text MatchResult;
    [SerializeField] GameObject MatchImg;
    [SerializeField] GameObject NoMatchImg;
    [SerializeField] TMP_Text Dialogue;
    
    [SerializeField] Transform TagContainer;
    [SerializeField] GameObject TagPrefab;

    [SerializeField] GameObject GoodEnding;
    [SerializeField] GameObject BadEnding;
    [SerializeField] GameObject MatchGroup;    

    void Awake() {
        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    public void Populate(StonkerData stonker, bool matched) {
        if (LoveManager.Inst.Lives < 0) {
            BadEnding.SetActive(true);
            GoodEnding.SetActive(false);
            MatchGroup.SetActive(false);
            return;
        } else if (stonker.Name == "Elon Musk" && matched) {
            BadEnding.SetActive(false);
            GoodEnding.SetActive(true);
            MatchGroup.SetActive(false);
            return;
        } else {
            BadEnding.SetActive(false);
            GoodEnding.SetActive(false);
            MatchGroup.SetActive(true);
        }

        MatchResult.text = matched ? "Matcheaste!" : "No matcheaste...";

        UserProfilePic.sprite = stonker.GetSprite();

        if (matched) {
            Dialogue.text = !string.IsNullOrEmpty(stonker.DialogueMatch) ? stonker.DialogueMatch : LoveManager.Inst.RandomDialogueMatch;
        } else {
            Dialogue.text = !string.IsNullOrEmpty(stonker.DialogueNoMatch) ? stonker.DialogueNoMatch : LoveManager.Inst.RandomDialogueNoMatch;
        }

        TagContainer.gameObject.SetActive(matched);

        if (matched) {
            foreach (Transform child in TagContainer) {
                Destroy(child.gameObject);
            }

            if (stonker.Tags != null) {
                foreach (var tag in stonker.Tags) {
                    var tagElement = Instantiate(TagPrefab, TagContainer).GetComponent<TagElement>();
                    tagElement.Populate(tag, true);
                }
            }
        }
        
        gameObject.SetActive(true);
    }

    public void Open() {
        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.DOFade(1f, 0.5f).onComplete += () => {
            canvasGroup.interactable = true;
        };
        canvasGroup.blocksRaycasts = true;
    }

    public void Close() {
        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.DOFade(0f, 0.5f).onComplete += () => {
            canvasGroup.blocksRaycasts = false;
        };
    }
}
