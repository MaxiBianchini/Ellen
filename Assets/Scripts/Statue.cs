using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    Vector3 RelativePos;
    private Transform Player;

    //[Range(0, 1)] public float ValorLerp;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Ellen").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 relativePos = Player.position - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        RelativePos = Player.position;
        RelativePos.y = 0;
        //RelativePos.x = 0;

        transform.LookAt(RelativePos);
        //transform.Rotate(0, transform.rotation.y, 0);
        //transform.rotation.x = new Quaternion;

        //transform.rotation = Quaternion.FromToRotation()
        //transform.rotation = Vector3.Lerp(transform.position, Player.position, ValorLerp););

        /*Vector3 direccion = Player.position - transform.position;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.up);*/
    }
}
