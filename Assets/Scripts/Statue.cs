using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    private Vector3 RelativePos;
    private Transform Player;

    void Start()
    {
        Player = GameObject.Find("Ellen").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 relativePos = Player.position - transform.position;

        RelativePos = Player.position;
        RelativePos.y = 0;

        transform.LookAt(RelativePos);
    }
}
