using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Interfejs : MonoBehaviour {

    public TextMeshProUGUI zegar;
    public TextMeshProUGUI hpTekst;
    
    public void PokażZegar (float sekundy) {
        TimeSpan time = TimeSpan.FromSeconds(sekundy);
        zegar.text = time.ToString(@"mm\:ss\:fff");
    }
    
    public void PokażHp (float hp) {
        hpTekst.text = hp.ToString();
    }
       
}
