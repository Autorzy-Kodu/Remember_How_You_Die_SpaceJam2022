using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NajlepszeWyniki : MonoBehaviour {
    
    public TextMeshProUGUI teskt;
    public DateTime start;
    
    void Start() {
        DontDestroyOnLoad (gameObject);
        TimeSpan time = TimeSpan.FromSeconds(PlayerPrefs.GetFloat ("najlepszy", 3599f));
        teskt.text = "Najlepszy czas: " + time.ToString(@"mm\:ss\:fff");
        start = DateTime.Now;
    }
    
    public void ZapiszWynik () {
        float wynik = (float)(DateTime.Now - start).TotalSeconds;
        if (wynik < PlayerPrefs.GetFloat ("najlepszy", 3599f)) {
            PlayerPrefs.SetFloat ("najlepszy", wynik);
        }
        Destroy (gameObject);
    }
    
}