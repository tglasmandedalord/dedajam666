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

    int index;
    CompanyData company;

    public void OnEnable() {
        index = UnityEngine.Random.Range(0, LoveManager.Instance.companies.Length);
        SelectCompany(index);
    }

    public void SwipeLeft() {
        LoveManager.Instance.ChangeRating(company, -1);
        NextCompany();
    }

    public void SwipeRight() {
        LoveManager.Instance.ChangeRating(company, +1);
        NextCompany();
    }

    void NextCompany() {
        index = (int) Mathf.Repeat(index + 1, LoveManager.Instance.companies.Length);
        SelectCompany(index);
    }

    void SelectCompany(int index) {
        company = LoveManager.Instance.companies[index];

        UserName.text = company.Name;
        UserDesc.text = $"Rating: {LoveManager.Instance.GetRating(company)}";
    }
}
