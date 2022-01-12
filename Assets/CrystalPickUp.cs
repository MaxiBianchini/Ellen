using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {

            this.gameObject.SetActive(false);
            FindObjectOfType<WinGame>().CristalFinal();
            // Medallero.GetComponent<JuegoGanado>().NuevaMedalla();
            //GetComponent<JuegoGanado>().NuevaMedalla(); // TIENE QUE INCREMENTAR EL CONTADOR DE MEDALLAS. NECESITA 3 PARA PODER GANAR EL JUEGO
            
        }
    }
}
