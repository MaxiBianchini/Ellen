using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithFloor : MonoBehaviour
{
    CharacterController Player;

    private Vector3 PosicionSuelo;
    private Vector3 UltimaPosicionSuelo;
    private string NombreSuelo;
    private string UltimoNombreSuelo;

    private float FactDiv;
    private Vector3 OriginOffset;

    // Start is called before the first frame update
    void Start()
    {
        OriginOffset = new Vector3(0, 1, 0);

        FactDiv = 4.2f;
        Player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player.isGrounded)
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position + OriginOffset, Player.height / FactDiv, -transform.up, out hit))
            {
                GameObject SueloActual = hit.collider.gameObject;
                NombreSuelo = SueloActual.name;
                PosicionSuelo = SueloActual.transform.position;

                if (PosicionSuelo != UltimaPosicionSuelo && NombreSuelo == UltimoNombreSuelo) transform.position += PosicionSuelo - UltimaPosicionSuelo;

                UltimoNombreSuelo = NombreSuelo;
                UltimaPosicionSuelo = PosicionSuelo;
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
        Gizmos.DrawWireSphere(transform.position + OriginOffset, Player.height / FactDiv);
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
