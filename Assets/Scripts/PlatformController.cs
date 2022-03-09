using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{ 
    // Variables para la plataforma y su movimiento
    public Rigidbody Plataforma;
    public Transform[] PosicionesPlataorma;
    private float Velocidad;

    private int PosicionActual;
    private int PosicionSiguiente;

    private bool MoverSiguientePos;
    private float TiempoEspera;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializacion de variables
        MoverSiguientePos = true;
        TiempoEspera = 2;
        Velocidad = 4;

        PosicionActual = 0;
        PosicionSiguiente = 1;
    }

    // Update is called once per frame
    void Update()
    {
        MoverPlataforma(); //LLamado a la funcion MoverPlataforma()
    }

    private void MoverPlataforma()
    {
        if (MoverSiguientePos)
        {
            StopCoroutine(EsperarParaMover(0f));//Termina el llamado a EsperarMover
            Plataforma.MovePosition(Vector3.MoveTowards(Plataforma.position, PosicionesPlataorma[PosicionSiguiente].position, Velocidad * Time.deltaTime)); //Genera que se mueva de la posicion actual a la siguiente posicion
        }

        if (Vector3.Distance(Plataforma.position, PosicionesPlataorma[PosicionSiguiente].position) <= 0) //Si la distancia entre la posicion de la plataforma y la posicion siguiente es 0, cambiamos la posicion actual
        {
            StartCoroutine(EsperarParaMover(TiempoEspera));//Cuando llegó a destino espera unos segundos

            //Cambia el valor de posicion actual y siguiente
            PosicionActual = PosicionSiguiente;
            PosicionSiguiente++;

            if (PosicionSiguiente > PosicionesPlataorma.Length - 1) PosicionSiguiente = 0; //Si llegó a la ultima posicion, volvemos a inicializar el arreglo
        }
    }

    //Al llamar a esta funcion, ésta se ejecuta el paralelo al otro codigo
    IEnumerator EsperarParaMover(float time)
    {
        MoverSiguientePos = false;
        yield return new WaitForSeconds(time); //Hace esperar cierto "time" para pasar a la siguiente linea
        MoverSiguientePos = true;
    }
}
