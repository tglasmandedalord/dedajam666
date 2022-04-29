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
    public string Photo;
    public string DialogueMatch;
    public string DialogueNoMatch;

    public TagData[] Tags;
}

[System.Serializable]
public class TagData {
    public string Name;
    public int Value;
    public string Icon;
}