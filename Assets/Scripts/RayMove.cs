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

        body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") Destroy(this.gameObject);
    }
}
