using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverConSuelo : MonoBehaviour
{
    CharacterController Player;

    Vector3 PosicionSuelo;
    Vector3 UltimaPosicionSuelo;
    string NombreSuelo;
    string UltimoNombreSuelo;

    public float FactDiv = 4.2f;
    public Vector3 OriginOffset;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.isGrounded)
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position + OriginOffset, Player.radius / FactDiv, -transform.up, out hit))
            {
                GameObject SueloActual = hit.collider.gameObject;
                NombreSuelo = SueloActual.name;
                PosicionSuelo = SueloActual.transform.position;

                if (PosicionSuelo != UltimaPosicionSuelo && NombreSuelo == UltimoNombreSuelo)
                {
                    transform.position += PosicionSuelo - UltimaPosicionSuelo;
                }

                UltimoNombreSuelo = NombreSuelo;
                UltimaPosicionSuelo = PosicionSuelo;

                //this.transform.parent = SueloActual.transform;


            }

        }
        else if (!Player.isGrounded)
        {
            UltimoNombreSuelo = null;
            UltimaPosicionSuelo = Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Player = GetComponent<CharacterController>();
        Gizmos.DrawWireSphere(transform.position + OriginOffset, Player.radius / FactDiv);
    }
}
