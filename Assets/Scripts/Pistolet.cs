using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistolet : MonoBehaviour {
    
    public AudioSource strzał;
    public AudioClip klip;
    public GameObject rozbłysk;
    
    public void Wystrzał () {
        StartCoroutine ("Strzel");
    }
    
    IEnumerator Strzel () {
        if (strzał)
            strzał.PlayOneShot(klip);
        
        rozbłysk.SetActive (true);
        yield return new WaitForSecondsRealtime (0.1f);
        rozbłysk.SetActive (false);
    }
}