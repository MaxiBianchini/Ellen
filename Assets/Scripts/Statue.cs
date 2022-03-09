using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    //Variables varias
    private Vector3 RelativePos;
    private Transform Player;

    void Start()
    {
        Player = GameObject.Find("Ellen").transform; //Obtenemos el transform del Player
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 relativePos = Player.position - transform.position; // Obtiene un vector direccion entre la estatua y el personaje

        RelativePos = Player.position;
        RelativePos.y = 0; //Fija el valor de y

        transform.LookAt(RelativePos); //Hace que el objeto mire hacia donde esta el player
    }
}
