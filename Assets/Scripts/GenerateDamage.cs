using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDamage : MonoBehaviour
{
    private int cantidad;

    private void Start()
    {
        cantidad = 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && this.tag == "Chomper")  other.GetComponent<Lives>().RestarVida(cantidad);

        else if (other.tag == "Player" && this.tag == "Ray")
        {
            other.GetComponent<Lives>().RestarVida(cantidad);
            Destroy(this.gameObject);

        }
        else if (other.tag == "Player" && this.tag == "Water") other.GetComponent<Lives>().RestarVida(3);
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && this.tag == "Chomper") other.GetComponent<Lives>().RestarVida(cantidad);
    }
}
