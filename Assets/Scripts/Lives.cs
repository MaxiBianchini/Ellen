using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour
{
    private CharacterController Player;

    public Vector3 PosInicial;

    public int Vidas = 3;
    private bool Invencible = false;

    public float TiempoInvecible = 1f;
    public float TiempoInmovil = 0.2f;

    private Animator Animacion;

    private void Start()
    {
        Player = GetComponent<CharacterController>();
        Animacion = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void RestarVida(int cantidad)
    {
        if (!Invencible && Vidas > 0)
        {
            Vidas -= cantidad;

            Animacion.Play("Daño");

            Invencible = true;
            StartCoroutine(Invulnerabilidad());
            StartCoroutine(FrenarVelocidad());
        }
        else if (Vidas == 0) SceneManager.LoadScene("Nivel_1"); //Cargar el menu de perder.
        
    }

    IEnumerator Invulnerabilidad()
    {
        Invencible = true;
        yield return new WaitForSeconds(TiempoInvecible);
        Invencible = false;
    }

    IEnumerator FrenarVelocidad()
    {
        var VelocidadActual = GetComponent<PlayerController>().Velocidad;
        GetComponent<PlayerController>().Velocidad = 0;
        yield return new WaitForSeconds(TiempoInmovil);
        GetComponent<PlayerController>().Velocidad = VelocidadActual;
    }
}
