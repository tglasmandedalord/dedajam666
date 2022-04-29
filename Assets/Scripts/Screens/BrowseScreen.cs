using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class BrowseScreen : MonoBehaviour
{
    [SerializeField] Image UserProfilePic;
    [SerializeField] TMP_Text UserName;
    [SerializeField] TMP_Text UserDesc;
    [SerializeField] TMP_Text UserCompanies;
    [SerializeField] TMP_Text UserNetWorth;
    
    [SerializeField] Transform TagContainer;
    [SerializeField] GameObject TagPrefab;

    [SerializeField] MatchPopup MatchPopup;

    StonkerData stonker;

    public void Awake() {
        SelectStonker();
    }

    public void SwipeLeft() {
        Debug.Log("Swiped left");
        ShowResult(false);
    }

    public void SwipeRight() {
        Debug.Log("Swiped right");
        ShowResult(LoveManager.Inst.TryMatch(stonker));
    }

    void ShowResult(bool liked) {
        MatchPopup.Open();
        MatchPopup.Populate(stonker, liked);

        if (stonker.Name == "Elon Musk") {
            if (LoveManager.Inst.FirstLife) {
                LoveManager.Inst.FirstLife = false;
            } else {
                // LoveManager.Inst.LoseLife();
            }
        }
    }

    public void HideResult() {
        MatchPopup.Close();
        SelectStonker();
    }

    void SelectStonker() {
        LoveManager.Inst.ElonCounter++;
        if (LoveManager.Inst.ElonCounter >= 5) {
            LoveManager.Inst.ElonCounter = 0;
            stonker = LoveManager.Inst.AllStonkers.First(s => s.Name == "Elon Musk");
        } else {
            stonker = LoveManager.Inst.GetRandomStonker();
        }

        UserName.text = $"{stonker.Name}({stonker.Age})";
        UserDesc.text = stonker.Desc;
        UserNetWorth.text = stonker.Networth;
        UserCompanies.text = stonker.Companies;
        UserProfilePic.sprite = stonker.GetSprite();

        foreach (Transform child in TagContainer) {
            Destroy(child.gameObject);
        }

        if (stonker.Tags != null) {
            foreach (var tag in stonker.Tags) {
                var tagElement = Instantiate(TagPrefab, TagContainer).GetComponent<TagElement>();
                tagElement.Populate(tag);
            }
        }
    }
}
