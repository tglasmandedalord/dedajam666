using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoveManager : MonoBehaviour
{
    Dictionary<StonkerData, int> ratingLookup;

    public List<TagData> PlayerTags;
    public List<StonkerData> Stonkers;
    public List<StonkerData> MatchedStonkers;
    public List<string> DialogueMatchGeneric = new List<string>();
    public List<string> DialogueNoMatchGeneric = new List<string>();
    public string RandomDialogueMatch => DialogueMatchGeneric[UnityEngine.Random.Range(0, DialogueMatchGeneric.Count)];
    public string RandomDialogueNoMatch => DialogueNoMatchGeneric[UnityEngine.Random.Range(0, DialogueNoMatchGeneric.Count)];

    StonkersDatabase stonkers;

    public static LoveManager Inst;

    void Awake() {
        ratingLookup = new Dictionary<StonkerData, int>();

        var stonkersFile = Resources.Load<TextAsset>("stonkers");
        stonkers = JsonUtility.FromJson<StonkersDatabase>(stonkersFile.text);
        Stonkers = stonkers.Stonkers.ToList();

        PlayerTags = new List<TagData>();
        foreach (var tag in Stonkers.SelectMany(s => s.Tags)) {
            if (!PlayerTags.Any(t => t.Name == tag.Name)) {
                var emptyTag = new TagData(tag.Name);
                PlayerTags.Add(emptyTag);
            }
        }

        var randomTag = UnityEngine.Random.Range(0, PlayerTags.Count);
        PlayerTags[randomTag].Value += 5;

        randomTag = UnityEngine.Random.Range(0, PlayerTags.Count);
        PlayerTags[randomTag].Value += 5;

        randomTag = UnityEngine.Random.Range(0, PlayerTags.Count);
        PlayerTags[randomTag].Value += 5;

        if (Inst == null) {
            Inst = this;
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

        if (UnityEngine.Random.Range(0f, 1f) < chance) {
            ratingLookup[stonker]++;

            MatchedStonkers.Add(stonker);

            foreach (var tag in stonker.Tags) {
                var playerTag = PlayerTags.FirstOrDefault(t => t.Name == tag.Name);
                if (playerTag != null) {
                    playerTag.Value += tag.Value;
                }
            }
            
            Debug.Log(playerValue + "/" + stonkerValue + "=" + chance + "--- Match!"); 
            return true;
        } else {
            Debug.Log(playerValue + "/" + stonkerValue + "=" + chance + "--- No Match!"); 
            return false;
        }
    }
}
