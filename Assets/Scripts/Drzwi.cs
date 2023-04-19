using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drzwi : Używalne {
    
    public Animator anim;
    
    // otwórz
    public override void Użyj () {
        anim.SetTrigger ("otwórz");
    }
}