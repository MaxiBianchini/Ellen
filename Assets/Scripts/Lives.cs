using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour
{
    //Variables varias
    private CharacterController Player;

    public GameObject[] Corazones;

    private int Vidas;
    private bool Invencible;

    private float TiempoInvecible;
    private float TiempoInmovil;

    private Animator Animacion;

    private void Start()
    {
        //Inicializacion de variables
        Player = GetComponent<CharacterController>();
        Animacion = GetComponent<Animator>();

        Vidas = 3;
        Invencible = false;

        TiempoInvecible = 1f;
        TiempoInmovil = 0.2f;
    }

    void Update()
    {

    }

    public void RestarVida(int cantidad) //Fncion de restar vida cada vez que hay collision con algun enemigo
    {
        if (!Invencible && Vidas > 0) //Si no es invencible pueden hacerle daño
        {
            Vidas -= cantidad;
            Destroy(Corazones[Vidas].gameObject); //Elimina uno de los corazones del UI

            Animacion.Play("Daño"); //Activa la animacion de daño

            Invencible = true;
            StartCoroutine(Invulnerabilidad()); //Llama a la cortina de invulnerabilidad
            StartCoroutine(FrenarVelocidad()); //Llama a la cortina de frenarvelocidad
        }
        
        if (Vidas == 0 || Vidas < 0) FindObjectOfType<GameController>().Lose(); //Si se quedó sin vidas pierde el juego y sale el menu Lose
    }

    public float VerVidas()
    {
        return Vidas;
    }

    //Lo hace invencible durante un segundo para darle tiempo de escapar
    IEnumerator Invulnerabilidad()
    {
        Invencible = true;
        yield return new WaitForSeconds(TiempoInvecible);
        Invencible = false;
    }

    //Frena al jugador para que el daño sea mas notorio
    IEnumerator FrenarVelocidad()
    {
        var VelocidadActual = GetComponent<PlayerController>().Velocidad;
        GetComponent<PlayerController>().Velocidad = 0;
        yield return new WaitForSeconds(TiempoInmovil);
        GetComponent<PlayerController>().Velocidad = VelocidadActual;
    }
}
