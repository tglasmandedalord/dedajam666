using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TagElement : MonoBehaviour {
    [SerializeField] Image Icon;
    [SerializeField] TMP_Text Name;

    public void Populate(TagData tag) {
        Icon.sprite = tag.GetSprite();
        Name.text = $"{tag.Name} {tag.Value}";
    }
}