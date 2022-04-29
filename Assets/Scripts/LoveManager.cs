using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveManager : MonoBehaviour
{
    Dictionary<StonkerData, int> ratingLookup;
    Dictionary<TagData, int> playerTags;

    public StonkerData[] Stonkers;
    StonkersDatabase stonkers;

    public static LoveManager Instance;

    void Awake() {
        ratingLookup = new Dictionary<StonkerData, int>();
        playerTags = new Dictionary<TagData, int>();

        var stonkersFile = Resources.Load<TextAsset>("stonkers");
        stonkers = JsonUtility.FromJson<StonkersDatabase>(stonkersFile.text);
        Stonkers = stonkers.Stonkers;

        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this);
        }
    }

    public void ChangeRating(StonkerData stonker, int ratingChange) {
        if (ratingLookup.ContainsKey(stonker)) {
            ratingLookup[stonker] += ratingChange;
        } else {
            ratingLookup.Add(stonker, ratingChange);
        }
    }

    public int GetRating(StonkerData stonker) => 
        ratingLookup.ContainsKey(stonker) ? ratingLookup[stonker] : 0;
    
    public bool TryMatch(StonkerData stonker) {
        var stonkerValue = 0f;
        var playerValue = 0f;

        foreach (var tag in stonker.Tags) {
            stonkerValue += tag.Value;
            
            if (playerTags.ContainsKey(tag)) {
                playerValue += playerTags[tag];
            }
        }

        var chance = playerValue / stonkerValue;
        Debug.Log(playerValue + "/" + stonkerValue + "=" + chance);

        if (UnityEngine.Random.Range(0f, 1f) < chance) {
            ratingLookup[stonker]++;
            return true;
        } else {
            return false;
        }
    }
}
