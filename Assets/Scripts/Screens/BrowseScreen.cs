using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    int index;
    StonkerData stonker;

    public void OnEnable() {
        index = UnityEngine.Random.Range(0, LoveManager.Instance.Stonkers.Length);
        SelectStonker(index);
        MatchPopup.gameObject.SetActive(false);
    }

    public void SwipeLeft() {
        LoveManager.Instance.ChangeRating(stonker, -1);
        ShowResult(false);
    }

    public void SwipeRight() {
        LoveManager.Instance.ChangeRating(stonker, +1);
        ShowResult(true);
    }

    void ShowResult(bool liked) {
        MatchPopup.gameObject.SetActive(true);
        MatchPopup.Populate(stonker, liked);
    }

    public void HideResult() {
        MatchPopup.gameObject.SetActive(false);
        NextStonker();
    }

    public void NextStonker() {
        index = (int) Mathf.Repeat(index + 1, LoveManager.Instance.Stonkers.Length);
        SelectStonker(index);
    }

    void SelectStonker(int index) {
        stonker = LoveManager.Instance.Stonkers[index];

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
