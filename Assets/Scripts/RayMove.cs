using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayMove : MonoBehaviour
{
    private float speed;
    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        speed = 20;  

        // Obtiene el cuerpo del objeto  que lo crea para tener su direccion y asi ser disparado
        body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * speed;
    }

    // Al colisionar con el personaje, el Ray se destruye
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") Destroy(this.gameObject);
    }
}
