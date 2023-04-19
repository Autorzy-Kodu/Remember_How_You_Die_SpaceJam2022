using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Poziom : MonoBehaviour {
    
    public Gracz gracz;
    public List<GameObject> maćki;
    public bool czyKoniec = false;
    
    public string nazwaNastępnegoPoziomu;
    
    public void Resetuj () {
        Debug.Log ("resetuj");
        gracz.Resetuj ();
        for (int i = 0; i < maćki.Count; i++) {
            maćki[i].GetComponent<AI>().hp = 100f;
            maćki[i].SetActive (true);
            maćki[i].GetComponent<AI>().Resetuj ();
            maćki[i].GetComponent<AI>().pamieć.Uruchom ();
        }
    }
    
    public void NastępnyPoziom () {
        if (czyKoniec)
            GameObject.Find ("NajlepszeWyniki").GetComponent<NajlepszeWyniki>().ZapiszWynik ();
        SceneManager.LoadScene (nazwaNastępnegoPoziomu);
    }
    
    void Update () {
        
        int aktywneMaćki = 0;
        foreach (GameObject maciek in maćki) {
            if (maciek.activeSelf) {
                aktywneMaćki++;
            }
        }
        if (aktywneMaćki == 0) {
            NastępnyPoziom ();
        }
        
    }
    
}
