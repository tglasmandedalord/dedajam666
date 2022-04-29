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

    [SerializeField] GameObject MatchResultGO;
    [SerializeField] TMP_Text MatchResult;

    int index;
    StonkerData stonker;

    public void OnEnable() {
        index = UnityEngine.Random.Range(0, LoveManager.Instance.Stonkers.Length);
        SelectCompany(index);
        MatchResultGO.SetActive(false);
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
        MatchResultGO.SetActive(true);
        MatchResult.text = liked && LoveManager.Instance.TryMatch(stonker) ? "Match!" : "No match :(";
    }

    public void HideResult() {
        MatchResultGO.SetActive(false);
        NextStonker();
    }

    public void NextStonker() {
        index = (int) Mathf.Repeat(index + 1, LoveManager.Instance.Stonkers.Length);
        SelectCompany(index);
    }

    void SelectCompany(int index) {
        stonker = LoveManager.Instance.Stonkers[index];

        UserName.text = stonker.Name;
        UserDesc.text = stonker.Desc;
        if (!string.IsNullOrEmpty(stonker.Photo)) {
            var photo = Resources.Load<Texture2D>("Photos/" + stonker.Photo);
            UserProfilePic.sprite = Sprite.Create(photo, new Rect(0,0,photo.width, photo.height), new Vector2(0.5f, 0.5f));
        }
    }
}
