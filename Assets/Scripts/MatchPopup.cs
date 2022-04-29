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
    [SerializeField] Image DialoguePic;
    [SerializeField] TMP_Text MatchResult;
    [SerializeField] TMP_Text Dialogue;
    
    [SerializeField] Transform TagContainer;
    [SerializeField] GameObject TagPrefab;

    public void Populate(StonkerData stonker, bool matched) {
        MatchResult.text = matched ? "Match!" : "No match :(";

        UserProfilePic.sprite = stonker.GetSprite();
        DialoguePic.sprite = stonker.GetSprite();

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
