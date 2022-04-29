using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TagElement : MonoBehaviour {
    [SerializeField] Image Icon;
    [SerializeField] TMP_Text Name;

    public void Populate(TagData tag) {
        if (!string.IsNullOrEmpty(tag.Icon)) {
            var icon = Resources.Load<Texture2D>("Icons/" + tag.Icon);
            Icon.sprite = Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), new Vector2(0.5f, 0.5f));
        }
        Name.text = tag.Name;
    }
}