using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoveManager : MonoBehaviour
{
    public List<TagData> PlayerTags;
    public int PlayerLevel => PlayerTags.Sum(t => t.Value);
    public List<StonkerData> Stonkers;
    public List<StonkerData> MatchedStonkers;
    public List<string> DialogueMatchGeneric = new List<string>();
    public List<string> DialogueNoMatchGeneric = new List<string>();
    public string RandomDialogueMatch => DialogueMatchGeneric[UnityEngine.Random.Range(0, DialogueMatchGeneric.Count)];
    public string RandomDialogueNoMatch => DialogueNoMatchGeneric[UnityEngine.Random.Range(0, DialogueNoMatchGeneric.Count)];

    StonkersDatabase stonkers;

    public static LoveManager Inst;

    void Awake() {
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

    StonkerData lastStonker;
    public StonkerData GetRandomStonker() {
        var candidates = Stonkers.Where(s => PlayerLevel / s.Level >= 0.3f).ToList();

        var candidate = Random.Range(0, candidates.Count);
        lastStonker = candidates[candidate];
        Debug.Log("Chosen stonker: " + candidates[candidate].Name);
        return lastStonker;
    }
    
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
