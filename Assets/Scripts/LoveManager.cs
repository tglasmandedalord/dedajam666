using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoveManager : MonoBehaviour
{
    public List<TagData> PlayerTags;
    public int PlayerLevel => PlayerTags.Sum(t => t.Value);
    public List<StonkerData> AvailableStonkers;
    public List<StonkerData> AllStonkers;
    public List<string> DialogueMatchGeneric = new List<string>();
    public List<string> DialogueNoMatchGeneric = new List<string>();
    public string RandomDialogueMatch => DialogueMatchGeneric[UnityEngine.Random.Range(0, DialogueMatchGeneric.Count)];
    public string RandomDialogueNoMatch => DialogueNoMatchGeneric[UnityEngine.Random.Range(0, DialogueNoMatchGeneric.Count)];

    public int ElonCounter = 5;
    public int Lives = 4;
    public bool FirstLife = true;

    [SerializeField] Image[] LifeImgs;

    StonkersDatabase stonkers;

    public static LoveManager Inst;

    void Awake() {
        var stonkersFile = Resources.Load<TextAsset>("stonkers");
        stonkers = JsonUtility.FromJson<StonkersDatabase>(stonkersFile.text);
        AvailableStonkers = stonkers.Stonkers.ToList();
        AllStonkers = stonkers.Stonkers.ToList();

        PlayerTags = new List<TagData>();
        foreach (var tag in AvailableStonkers.SelectMany(s => s.Tags)) {
            if (!PlayerTags.Any(t => t.Name == tag.Name)) {
                var emptyTag = new TagData(tag.Name);
                emptyTag.Value = Random.Range(1, 4);
                PlayerTags.Add(emptyTag);
            }
        }

        // var randomTag = UnityEngine.Random.Range(0, PlayerTags.Count);
        // PlayerTags[randomTag].Value += 5;

        // randomTag = UnityEngine.Random.Range(0, PlayerTags.Count);
        // PlayerTags[randomTag].Value += 5;

        // randomTag = UnityEngine.Random.Range(0, PlayerTags.Count);
        // PlayerTags[randomTag].Value += 5;

        Inst = this;
    }

    public void RestartGame() {
        SceneManager.LoadScene(0);
    }

    List<StonkerData> SortedStonkers;
    StonkerData lastStonker;
    public StonkerData GetRandomStonker() {
        var candidates = AvailableStonkers.OrderBy(s => s.Level).ToList();

        var candidate = Random.Range(0, Mathf.Min(10, candidates.Count));
        while (candidates[candidate] == lastStonker && candidates.Count > 1) {
            candidate = Random.Range(0, Mathf.Min(10, candidates.Count));
        }
        lastStonker = candidates[candidate];
        Debug.Log("Chosen stonker: " + lastStonker.Name);
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

        if (!FirstLife && UnityEngine.Random.Range(0f, 1f) < chance) {
            AvailableStonkers.Remove(stonker);

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
            if (stonker.Name == "Elon Musk") {
                if (FirstLife) {
                    FirstLife = false;
                } else {
                    LoseLife();
                }
            }

            return false;
        }
    }

    public void LoseLife() {
        Lives--;

        for (var i = 0; i < 5; i++) {
            LifeImgs[i].gameObject.SetActive(Lives >= i);
        }
    }
}
