using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottonsController : MonoBehaviour
{

    //Botones que aparecen en los menús
    public void Jugar()
    {
        SceneManager.LoadScene(1); 
    }
    public void Salir()
    {
        Application.Quit();
    }
}
