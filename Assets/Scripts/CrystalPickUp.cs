using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (FindObjectOfType<GameController>().CristalFinal()) Destroy(this.gameObject);
        }
    }
}
