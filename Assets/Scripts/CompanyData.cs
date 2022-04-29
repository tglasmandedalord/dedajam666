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
    public string DialogueMatch;
    public string DialogueNoMatch;

    public TagData[] Tags;

    public Sprite GetSprite() {
        var tex = Resources.Load<Texture2D>("Photos/" + Name);
        if (tex != null) {
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
        return null;
    }
}

[System.Serializable]
public class TagData {
    public string Name;
    public int Value;

    public TagData(string n) {
        Name = n;
    }

    public Sprite GetSprite() {
        var tex = Resources.Load<Texture2D>("Icons/" + Name);
        if (tex != null) {
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
        return null;
    }
}