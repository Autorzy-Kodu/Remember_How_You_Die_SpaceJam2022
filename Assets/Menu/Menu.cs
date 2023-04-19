using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    
    public void Start () {
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void PrzyciskStart () {
        SceneManager.LoadScene ("Warehouse");
    }
    
    public void PrzyciskWyjście () {
        Application.Quit ();
    }
    
}