using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRay : MonoBehaviour
{
    public GameObject Rayo;

    private float Tiempo;
    private float Cronometro;

    // Start is called before the first frame update
    void Start()
    {
        Cronometro = 0;
        Tiempo = Random.Range(1, 4); //Valor de creacion Random entre 1 y 4
    }

    // Update is called once per frame
    void Update()
    {
        Cronometro += Time.deltaTime; //Cromonetro de creacion

        if (Cronometro >= Tiempo) CrearNuevoRayo(); //Llamado a funcion CrearNuevoRayo()
    }

    void CrearNuevoRayo()
    {
        GameObject Ray = Instantiate(Rayo); //Instancia un nuevo rayo
        Ray.transform.position = this.transform.position; //obtiene  la posicion del objeto que lo creo
        Ray.transform.rotation = this.transform.rotation; //obtiene la rotacion del objeto que lo creo

        Cronometro = 0;
    }
}
