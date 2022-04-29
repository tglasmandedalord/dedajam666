using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoveManager : MonoBehaviour
{
    Dictionary<StonkerData, int> ratingLookup;

    public List<TagData> PlayerTags;
    public StonkerData[] Stonkers;
    StonkersDatabase stonkers;

    public static LoveManager Instance;

    void Awake() {
        ratingLookup = new Dictionary<StonkerData, int>();

        var stonkersFile = Resources.Load<TextAsset>("stonkers");
        stonkers = JsonUtility.FromJson<StonkersDatabase>(stonkersFile.text);
        Stonkers = stonkers.Stonkers;

        PlayerTags = new List<TagData>();
        foreach (var tag in Stonkers.SelectMany(s => s.Tags)) {
            if (!PlayerTags.Any(t => t.Name == tag.Name)) {
                var emptyTag = new TagData(tag.Name, tag.Icon);
                PlayerTags.Add(emptyTag);
            }
        }

        var randomTag = UnityEngine.Random.Range(0, PlayerTags.Count);
        PlayerTags[randomTag].Value += 5;

        randomTag = UnityEngine.Random.Range(0, PlayerTags.Count);
        PlayerTags[randomTag].Value += 5;

        randomTag = UnityEngine.Random.Range(0, PlayerTags.Count);
        PlayerTags[randomTag].Value += 5;

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
        var stonkerValue = 1f;
        var playerValue = 0f;

        if (stonker.Tags != null) {
            foreach (var tag in stonker.Tags) {
                stonkerValue += tag.Value;
                
                var playerTag = PlayerTags.FirstOrDefault(t => t.Name == tag.Name);
                if (playerTag != null) {
                    playerValue += playerTag.Value;
                }
            }
        }

        var chance = playerValue / stonkerValue;
        Debug.Log(playerValue + "/" + stonkerValue + "=" + chance);

        if (UnityEngine.Random.Range(0f, 1f) < chance) {
            ratingLookup[stonker]++;

            foreach (var tag in stonker.Tags) {
                var playerTag = PlayerTags.FirstOrDefault(t => t.Name == tag.Name);
                if (playerTag != null) {
                    playerTag.Value += tag.Value;
                }
            }

            return true;
        } else {
            return false;
        }
    }
}
