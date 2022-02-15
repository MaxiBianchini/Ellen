using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float Sensibilidad;

    private Vector3 Distancia;
    private Transform Player;
    private float ValorLerp; //Cuan rapido cambia de posicion la camara

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Ellen").transform;

        ValorLerp = 1;
        Sensibilidad = 1.5f;
        Distancia = new Vector3(0, 1.5f, -6);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Player.position + Distancia, ValorLerp);
        Distancia = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * Sensibilidad, Vector3.up) * Distancia;

        transform.LookAt(Player); //Acomoda el angulo automaticamente para mirar al jugador
    }
}
