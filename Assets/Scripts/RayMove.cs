using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayMove : MonoBehaviour
{
    public float speed;

    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
        body.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {


           
                other.GetComponent<Lives>().RestarVida(1);
            
            this.gameObject.SetActive(false);
        }
    }
}
