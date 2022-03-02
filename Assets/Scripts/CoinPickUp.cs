using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    private float y;

    private void Start()
    {
        y = 0;
    }
    private void Update()
    {
        //Rotacion de las monedas (detalle estetico)
        y += Time.deltaTime * 30;
        transform.rotation = Quaternion.Euler(0, y, 0);
    }

    // Colision con una moneda, se destruye y reduce contador de monedas
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameController>().NuevaMedalla();

            Destroy(this.gameObject);
        }
    }
}
