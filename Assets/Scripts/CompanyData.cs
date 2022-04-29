using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class StonkersDatabase {
    public StonkerData[] Stonkers;
}

[System.Serializable]
public class StonkerData {
    public string Name;
    public string Desc;
    public string Age;
    public string Networth;
    public string Companies;
    public string Photo;
    public string DialogueMatch;
    public string DialogueNoMatch;

    public TagData[] Tags;

    public Sprite GetSprite() {
        if (!string.IsNullOrEmpty(Photo)) {
            var tex = Resources.Load<Texture2D>("Photos/" + Photo);
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
        return null;
    }
}

[System.Serializable]
public class TagData {
    public string Name;
    public int Value;
    public string Icon;

    public TagData(string n, string i) {
        Name = n;
        Icon = i;
    }

    public Sprite GetSprite() {
        if (!string.IsNullOrEmpty(Icon)) {
            var tex = Resources.Load<Texture2D>("Icons/" + Icon);
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
        return null;
    }
}