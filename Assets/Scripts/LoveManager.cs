using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveManager : MonoBehaviour
{
    public CompanyData[] companies;
    Dictionary<CompanyData, int> ratingLookup;

    public static LoveManager Instance;

    void Awake() {
        ratingLookup = new Dictionary<CompanyData, int>();

        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this);
        }
    }
}
