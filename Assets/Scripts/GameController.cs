using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int Medallas;

    private GameObject PuertaFinal;

    bool LoseMenuActive;
    bool StopMenuActive;
    bool WinMenuActive;

    private GameObject LoseMenu;
    private GameObject StopMenu;
    private GameObject WinMenu;

    // Start is called before the first frame update
    void Start()
    {
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

        if (Input.GetKeyDown("escape"))
        {
            StopMenuActive = !StopMenuActive;
            StopMenu.SetActive(StopMenuActive);

            Time.timeScale = (StopMenuActive) ? 0 : 1f;
        }
    }

    public void Lose()
    {
        LoseMenuActive = !LoseMenuActive;
        LoseMenu.SetActive(LoseMenuActive);

        Time.timeScale = (LoseMenuActive) ? 0 : 1f;
    }

    public void NuevaMedalla()
    {
        Medallas -= 1;

        if (Medallas == 0)
        {
            PuertaFinal = GameObject.Find("Door");
            Destroy(PuertaFinal);
        }
    }

    public bool CristalFinal()
    {
        if (Medallas != 0) return false;

        WinMenuActive = !WinMenuActive;
        WinMenu.SetActive(WinMenuActive);

        Time.timeScale = (WinMenuActive) ? 0 : 1f;

        return true;
    }
}

