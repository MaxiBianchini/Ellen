using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottonsController : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Nivel_1");
    }

    public void Salir()
    {
        Application.Quit();
    }
}
