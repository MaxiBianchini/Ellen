using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    private GameObject Personaje;
    private NavMeshAgent Agente;

    private Animator Animacion;
    private bool Atacar;

    private float CronometroAnimacion;

    // public Rigidbody Plataforma;
    public Transform[] PosicionesPlataorma;
    //public float Velocidad;

    private int PosicionActual = 0;
    private int PosicionSiguiente = 1;

    private bool MoverSiguientePos;
    public float TiempoEspera;

    // Start is called before the first frame update
    void Start()
    {
        Agente = GetComponent<NavMeshAgent>();
        Personaje = GameObject.Find("Ellen");

        Animacion = GetComponent<Animator>();
        Atacar = false;

        MoverSiguientePos = true;
    }

    // Update is called once per frame
    void Update()
    {
        Movimineto();
    }

    private void Movimineto()
    {
        if (Atacar)
        {
            CronometroAnimacion += 1 * Time.deltaTime;

            if (CronometroAnimacion >= 1.7)
            {
                Animacion.SetBool("Atacar", false);
                Atacar = false;

                CronometroAnimacion = 0;
            }
        }

        if (Vector3.Distance(transform.position, Personaje.transform.position) > 15)
        {
            Agente.speed = 2.5f;

            if (MoverSiguientePos)
            {
                Animacion.SetBool("Correr", false);
                Animacion.SetBool("Atacar", false);

                Animacion.SetBool("Caminando", true);

                StopCoroutine(EsperarParaMover(0f));

                Agente.destination = PosicionesPlataorma[PosicionSiguiente].position;
                //Plataforma.MovePosition(Vector3.MoveTowards(Plataforma.position, PosicionesPlataorma[PosicionSiguiente].position, Velocidad * Time.deltaTime));
            }

            if (Vector3.Distance(transform.position, PosicionesPlataorma[PosicionSiguiente].position) <= 0.5)
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
        else
        {
            if (Vector3.Distance(transform.position, Personaje.transform.position) > 2.5f && !Atacar)
            {
                Animacion.SetBool("Caminando", false);
                Animacion.SetBool("Correr", true);
                Animacion.SetBool("Atacar", false);

                Agente.speed = 7f;

                Agente.destination = Personaje.transform.position;
            }
            else
            {
                Animacion.SetBool("Caminando", false);
                Animacion.SetBool("Correr", false);

                Animacion.SetBool("Atacar", true);
                Atacar = true;
            }
        }
    }

    IEnumerator EsperarParaMover(float time)
    {
        MoverSiguientePos = false;
        Animacion.SetBool("Caminando", false);
        yield return new WaitForSeconds(time);
        MoverSiguientePos = true;
    }
}
