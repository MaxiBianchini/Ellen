using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottonsController : MonoBehaviour
{
    public void Jugar()
    {
        
        SceneManager.LoadScene(1);
   
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("salio del juego");
    }
}
