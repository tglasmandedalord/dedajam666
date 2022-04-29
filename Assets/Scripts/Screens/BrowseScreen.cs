using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BrowseScreen : MonoBehaviour
{
    [SerializeField] Image UserProfilePic;
    [SerializeField] TMP_Text UserName;
    [SerializeField] TMP_Text UserDesc;

    [SerializeField] GameObject MatchResultGO;
    [SerializeField] TMP_Text MatchResult;

    int index;
    CompanyData company;

    public void OnEnable() {
        index = UnityEngine.Random.Range(0, LoveManager.Instance.companies.Length);
        SelectCompany(index);
    }

    public void SwipeLeft() {
        LoveManager.Instance.ChangeRating(company, -1);
        ShowResult(false);
    }

    public void SwipeRight() {
        LoveManager.Instance.ChangeRating(company, +1);
        ShowResult(true);
    }

    void ShowResult(bool liked) {
        MatchResultGO.SetActive(true);

        MatchResult.text = liked && LoveManager.Instance.CheckMatch(company) ? "Match!" : "No match :(";
    }

    public void HideResult() {
        MatchResultGO.SetActive(false);
    }

    public void NextCompany() {
        index = (int) Mathf.Repeat(index + 1, LoveManager.Instance.companies.Length);
        SelectCompany(index);
    }

    void SelectCompany(int index) {
        company = LoveManager.Instance.companies[index];

        UserName.text = company.Name;
        UserDesc.text = company.Desc;
        UserProfilePic.sprite = company.Icon;
    }
}
