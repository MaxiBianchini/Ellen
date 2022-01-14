using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
     public float Sensibilidad;

     public Vector3 Distancia;
     private Transform Player;
     [Range(0, 1)] public float ValorLerp; //Cuan rapido cambia de posicion la camara

     // Start is called before the first frame update
     void Start()
     {
         Player = GameObject.Find("Ellen").transform;
     }

     // Update is called once per frame
     void LateUpdate()
     {
         transform.position = Vector3.Lerp(transform.position, Player.position + Distancia, ValorLerp);
         Distancia = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * Sensibilidad, Vector3.up) * Distancia;

         transform.LookAt(Player); //Acomoda el angulo automaticamente
     }


   /* public float cameraSmoothingFactor = 1;
    public float lookUpMax = 60;
    public float lookDownMax = -60;

    public Transform cameratransform;

    private Quaternion camRotation;

    private void Start()
    {
        camRotation = transform.localRotation;
    }

    private void Update()
    {
        camRotation.x += Input.GetAxis("Mouse Y") * cameraSmoothingFactor * (-1);
        camRotation.y += Input.GetAxis("Mouse X") * cameraSmoothingFactor;

        camRotation.x = Mathf.Clamp(camRotation.x, lookDownMax, lookUpMax);

        transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);


    }
    */











}
