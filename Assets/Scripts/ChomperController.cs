using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChomperController : MonoBehaviour
{
    private GameObject Personaje;
    private NavMeshAgent Agente; //NavMeshAgent

    //Variables de animacion
    private Animator Animacion;
    private bool Atacar;

    private float CronometroAnimacion;

    //Variables de Posicionamineto
    public Transform[] PosicionesEnemigo;
    private int PosicionSiguiente;
    private int PosicionActual;
    
    private bool MoverSiguientePos;
    private float TiempoEspera;

    // Start is called before the first frame update
    void Start()
    {
        Agente = GetComponent<NavMeshAgent>(); //Obtiene el agente de navegacion para saber por donde puede moverse
        Personaje = GameObject.Find("Ellen"); //Obtiene el Gameobjject del personaje para luego poder seguirlo segun la distancia al mismo

        Animacion = GetComponent<Animator>(); //Obtiene su aminacion

        //Inicializacion de variables
        Atacar = false;

        PosicionActual = 0;
        PosicionSiguiente = 1;

        TiempoEspera = Random.Range(1, 5);

        MoverSiguientePos = true;
    }

    // Update is called once per frame
    void Update()
    {
        Movimineto(); //Llamado a la funcion Movimineto()
    }

    private void Movimineto()
    {
        if (Atacar) //Cuando el chomper ataca, le se da un tiempo de animacion, hasta desactivarlo
        {
            CronometroAnimacion += 1 * Time.deltaTime;

            if (CronometroAnimacion >= 1.7)
            {
                //Desactiva la animacion de atacar
                Animacion.SetBool("Atacar", false);
                Atacar = false;

                CronometroAnimacion = 0;
            }
        }

        if (Vector3.Distance(transform.position, Personaje.transform.position) > 15) //Cuando está lejos del player, camina
        {
            Agente.speed = 2.5f; //Se le asigana una velocidad al agente (chomper)

            if (MoverSiguientePos)
            {
                //Activa la animacion de caminar y desactiva las otras
                Animacion.SetBool("Correr", false);
                Animacion.SetBool("Atacar", false);

                Animacion.SetBool("Caminando", true);

                StopCoroutine(EsperarParaMover(0f)); //Cortina para parar al chomper

                Agente.destination = PosicionesEnemigo[PosicionSiguiente].position; //Se le asigna un destino al chomper (una posicion preestablecida)
            }

            if (Vector3.Distance(transform.position, PosicionesEnemigo[PosicionSiguiente].position) <= 0.5) //Si llegó a destino, cambia la posicion a donde debe ir
            {
                StartCoroutine(EsperarParaMover(TiempoEspera)); //Tiempo de espera antes de volver a moverse

                //Pasa a la siguiente posicion
                PosicionActual = PosicionSiguiente;
                PosicionSiguiente++;

                if (PosicionSiguiente > PosicionesEnemigo.Length - 1) PosicionSiguiente = 0; //Cuando el array llega al final lo reiniciamos
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, Personaje.transform.position) > 2.5f && !Atacar) //Si esta cerca del player empieza a correr
            {
                //Activa la animacion de correr y desactiva las otras
                Animacion.SetBool("Caminando", false);
                Animacion.SetBool("Correr", true);
                Animacion.SetBool("Atacar", false);

                Agente.speed = 7f; //Se le asigana una velocidad al agente (chomper)

                Agente.destination = Personaje.transform.position; //Se le asigana como destino la posicion del personaje para que lo siga
            }
            else //Si esta MUY cerca del player lo Ataca
            {
                //Activa la animacion de atacar y desactiva las otras
                Animacion.SetBool("Caminando", false);
                Animacion.SetBool("Correr", false);

                Animacion.SetBool("Atacar", true);
                Atacar = true;
            }
        }
    }

    IEnumerator EsperarParaMover(float time) //COrtina que se activa para frenar al personaje al llegar a destino
    {
        MoverSiguientePos = false;
        Animacion.SetBool("Caminando", false);
        yield return new WaitForSeconds(time);
        MoverSiguientePos = true;
    }
}
