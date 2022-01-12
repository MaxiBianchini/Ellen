using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public int Medallas; //CAMBIAR A PRIVATE UNA VEZ TERMINADO

    private GameObject PuertaFinal;

    // Start is called before the first frame update
    bool active;
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }
    public void NuevaMedalla()
    {
        Medallas -= 1;

        if (Medallas == 0)
        {
            /*canvas.enabled = true;

            active = !active;
            canvas.enabled = active;

            Time.timeScale = (active) ? 0 : 1f;*/

            PuertaFinal = GameObject.Find("Door");
            PuertaFinal.SetActive(false);


        }
    }

    public void CristalFinal()
    {
        canvas.enabled = true;

        active = !active;
        canvas.enabled = active;

        Time.timeScale = (active) ? 0 : 1f;
    }
}
