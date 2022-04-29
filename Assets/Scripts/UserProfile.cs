using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserProfile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    [SerializeField] BrowseScreen browseScreen;
    [SerializeField] float swipeDistance = 150;
    [SerializeField] int moveSpd = 100;

    RectTransform rt;
    bool isDragging;
    Vector2 iniPos;
    Vector2 targetPos;

    void Awake() {
        rt = transform as RectTransform;
        iniPos = rt.anchoredPosition;
        targetPos = iniPos;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData) {
        targetPos = new Vector2(rt.anchoredPosition.x + eventData.delta.x, rt.anchoredPosition.y);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData) {
        isDragging = false;

        var delta = Mathf.Abs(rt.anchoredPosition.x - iniPos.x);

        if (delta >= swipeDistance) {
            if (rt.anchoredPosition.x < iniPos.x) {
                browseScreen.SwipeLeft();
            } else {
                browseScreen.SwipeRight();
            }
        }

        targetPos = iniPos;
    }

    void Update() {
        rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, targetPos, Time.deltaTime * moveSpd);
    }
}
