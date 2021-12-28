using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDamage : MonoBehaviour
{
    private int cantidad = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Lives>().RestarVida(cantidad);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Lives>().RestarVida(cantidad);
        }
    }
}
