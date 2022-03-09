using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithFloor : MonoBehaviour
{
    //Variebles varias
    CharacterController Player;

    //Variables del suelo
    private Vector3 PosicionSuelo;
    private Vector3 UltimaPosicionSuelo;
    private string NombreSuelo;
    private string UltimoNombreSuelo;

    private float FactDiv;
    private Vector3 OriginOffset;

    // Start is called before the first frame update
    void Start()
    {
        OriginOffset = new Vector3(0, 1, 0); //Ajusta el origen del Ray

        FactDiv = 4.2f; //Ajusta el radio del spherecast
        Player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player.isGrounded)
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position + OriginOffset, Player.radius / FactDiv, -transform.up, out hit)) //Comprueba si el rayo detecta que esta en contacto con un objeto
            {
                GameObject SueloActual = hit.collider.gameObject; //Almacena el gameobject del objeto con el que está colisionando
                NombreSuelo = SueloActual.name; //Almacena el nombre del objeto con el que está colisionando
                PosicionSuelo = SueloActual.transform.position; //Almacena la posicion del objeto con el que está colisionando

                if (PosicionSuelo != UltimaPosicionSuelo && NombreSuelo == UltimoNombreSuelo) transform.position += PosicionSuelo - UltimaPosicionSuelo; //Mueve el personaje si el suelo se mueve

                //Reajusta los valores de las variables
                UltimoNombreSuelo = NombreSuelo; 
                UltimaPosicionSuelo = PosicionSuelo;
            }

        }
        else if (!Player.isGrounded) //Si no está tocando suelo no hace los calculos
        {
            UltimoNombreSuelo = null;
            UltimaPosicionSuelo = Vector3.zero;
        }
    }

    //Crea un gizmo (esferea) con el diametro del jugador
    private void OnDrawGizmos()
    {
        Player = GetComponent<CharacterController>();
        Gizmos.DrawWireSphere(transform.position + OriginOffset, Player.radius / FactDiv);
    }

    private void Awake() //Ajusta los FPS a 60
    {
        Application.targetFrameRate = 60;
    }
}
