using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pamięć : MonoBehaviour {
    
    public bool zapamiętany = false;
    public GameObject model;
    
    void Awake () {
        Uruchom ();
    }
    
    public void Uruchom () {
        if (zapamiętany) {
            model.SetActive (true);
        } else {
            model.SetActive (false);
        }
    }
    
}
