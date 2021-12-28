using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    public Rigidbody Plataforma;
    public Transform[] PosicionesPlataorma;
    public float Velocidad;

    private int PosicionActual = 0;
    private int PosicionSiguiente = 1;

    public bool MoverSiguientePos;
    public float TiempoEspera;

    // Start is called before the first frame update
    void Start()
    {
        MoverSiguientePos = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoverPlataforma();
    }

    private void MoverPlataforma()
    {

        if (MoverSiguientePos)
        {
            StopCoroutine(EsperarParaMover(0f));
            Plataforma.MovePosition(Vector3.MoveTowards(Plataforma.position, PosicionesPlataorma[PosicionSiguiente].position, Velocidad * Time.deltaTime));
        }

        if (Vector3.Distance(Plataforma.position, PosicionesPlataorma[PosicionSiguiente].position) <= 0)
        {
            StartCoroutine(EsperarParaMover(TiempoEspera));

            PosicionActual = PosicionSiguiente;
            PosicionSiguiente++;

            if (PosicionSiguiente > PosicionesPlataorma.Length - 1)
            {
                PosicionSiguiente = 0;
            }
        }
    }

    IEnumerator EsperarParaMover(float time)
    {
        MoverSiguientePos = false;
        yield return new WaitForSeconds(time);
        MoverSiguientePos = true;
    }
}
