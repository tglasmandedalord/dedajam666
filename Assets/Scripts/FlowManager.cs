using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour
{
    public static FlowManager Instance;

    public GameObject splash;
    public GameObject signup;
    public GameObject browse;
    public GameObject profile;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this);
        }

        splash.SetActive(true);
        signup.SetActive(true);
        browse.SetActive(false);
        profile.SetActive(false);
    }
}
