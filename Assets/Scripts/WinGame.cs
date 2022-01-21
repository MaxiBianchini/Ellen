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

            PuertaFinal = GameObject.Find("Door");                        
            Destroy(PuertaFinal);
        }
    }

    public bool CristalFinal()
    {
        if (Medallas != 0) return false;

        canvas.enabled = true;

        active = !active;
        canvas.enabled = active;

        Time.timeScale = (active) ? 0 : 1f;

        return true;
    }
}
