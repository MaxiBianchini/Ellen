using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int Medallas;

    private GameObject PuertaFinal;

    public GameObject[] Monedas;

    //GameObjeccts y Variables de los distintos menús
    bool LoseMenuActive;
    bool StopMenuActive;
    bool WinMenuActive;
    
    private GameObject LoseMenu;
    private GameObject StopMenu;
    private GameObject WinMenu;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializacion de Variables y Obtencion de los GameObjects de los menús
        Medallas = 7;

        LoseMenuActive = false;
        LoseMenu = transform.GetChild(0).gameObject;
        LoseMenu.SetActive(LoseMenuActive);

        StopMenuActive = false;
        StopMenu = transform.GetChild(1).gameObject;
        StopMenu.SetActive(StopMenuActive);

        WinMenuActive = false;
        WinMenu = transform.GetChild(2).gameObject;
        WinMenu.SetActive(WinMenuActive);

        Time.timeScale = (false) ? 0 : 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (WinMenuActive || LoseMenuActive) return;

        if (Input.GetKeyDown("escape")) //activacion de menú de Pausa (ESC)
        {
            StopMenuActive = !StopMenuActive;
            StopMenu.SetActive(StopMenuActive);

            Time.timeScale = (StopMenuActive) ? 0 : 1f; //Detiene los movimeintos del juego
        }
    }

    public void Lose() //Funcion que maneja el Menú Lose
    {
        LoseMenuActive = !LoseMenuActive;
        LoseMenu.SetActive(LoseMenuActive);

        Time.timeScale = (LoseMenuActive) ? 0 : 1f; //Detiene los movimeintos del juego
    }

    public void NuevaMedalla() //Funcion que maneja el contador de monedas y  abre la puerta del templo (monedas = 0)
    {
        Medallas -= 1;
        Destroy(Monedas[Medallas].gameObject);

        if (Medallas == 0)
        {
            PuertaFinal = GameObject.Find("Door");
            Destroy(PuertaFinal);
        }
    }

    public bool CristalFinal() //Funcion que maneja el Menú Win una vez que alcanza el Cristal Final
    {
        if (Medallas != 0) return false;

        WinMenuActive = !WinMenuActive;
        WinMenu.SetActive(WinMenuActive);

        Time.timeScale = (WinMenuActive) ? 0 : 1f; //Detiene los movimeintos del juego

        return true;
    }
}

