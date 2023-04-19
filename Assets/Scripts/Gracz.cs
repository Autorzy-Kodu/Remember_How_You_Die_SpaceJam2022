using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Gracz : MonoBehaviour {
    
    public Poziom poziom;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
    public Pistolet pistolet;
    public float limitCzasu;
    public float zegar;
    
    public Camera kamera;
    public Interfejs interfejs;
    // odległość na jaką gracz może robić interakcje
    public float zasięgGracza = 2f;
    public float zasięgStrzału = 500f;
    public float Hp = 100;
    
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
    }
    
    public void Resetuj () {
        transform.position = spawnPosition;
        transform.Rotate (spawnRotation.eulerAngles, Space.World);
        transform.rotation = spawnRotation;
        Hp = 100f;
        interfejs.PokażHp (100f);
        zegar = limitCzasu;
    }
    
    void Update () {
        zegar -= Time.deltaTime;
        interfejs.PokażZegar (zegar);
    }
    
    void FixedUpdate () {
        if (Hp <= 0) {
            poziom.Resetuj ();
        }
        if (zegar <= 0) {
            poziom.Resetuj ();
        }
    }
    
    void OnStrzał () {
        RaycastHit hit;
        Physics.Raycast (kamera.transform.position, kamera.transform.forward, out hit, zasięgStrzału);
        
        pistolet.Wystrzał ();
        
        if (!hit.collider)
            return;
        
        AI ai = hit.collider.GetComponent<AI>();
        
        if (!ai)
            return;
            
        ai.hp -= Random.Range (30, 80);
        
    }
    
    void OnUse () {
        // jeśli klikneliśmy to niech pokaże to menu
        
        RaycastHit hit;
        Physics.Raycast (kamera.transform.position, kamera.transform.forward, out hit, zasięgGracza);
        if (!hit.collider)
            return;
        
        if (hit.collider.GetComponent<Używalne>())
            hit.collider.GetComponent<Używalne>().Użyj ();
    }
    
    void OnReset () {
        zegar = 0;
    }
    
}