using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
{

    private PlayerController Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Player.AddMovementInput(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
    }
}
