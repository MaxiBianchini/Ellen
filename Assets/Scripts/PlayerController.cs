using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     //Variables movimiento
     private CharacterController Player;

     private Vector3 PlayerInput;
     private float MoveH;
     private float MoveV;

     private float FuerzaSalto;

     public float Velocidad;
     private Vector3 DireccionPlayer;

     private float Gravedad;
     private float VelocidadCaida;

     private bool PuedoSaltar;

     //Variables movimiento relativo a camara
     public Camera Cam;
     private Vector3 CamDelante;
     private Vector3 CamDerecha;

     //Variables deslizamiento en pendiente
     private bool EstaEnPendiente;
     private float VelocidadPendiente;
     private float FuerzaPendiente;
     private Vector3 HitNormal;

     //Variables Animaciones
     private Animator PlayerAmimatorController;

     //Carga el componente CharacterController en la variable Player y el componente Animator al iniciar el script
     void Start()
     {
        Player = GetComponent<CharacterController>();
        PlayerAmimatorController = GetComponent<Animator>();

        //Cursor.visible = false; NO TE OVIDES DE ACTIVARLO

        FuerzaSalto = 10;
        Velocidad = 10;
        Gravedad = 40;

        EstaEnPendiente = false;
        VelocidadPendiente = 5f;
        FuerzaPendiente = -15f;

        PuedoSaltar = false;
     }

     // Update is called once per frame
     void Update()
     {
         //Guarda el valor de entrada Horizontal y Vertical para el movimineto
         MoveH = Input.GetAxis("Horizontal");
         MoveV = Input.GetAxis("Vertical");

         PlayerInput = new Vector3(MoveH, 0, MoveV); //Almacena los valores en un Vector3
         PlayerInput = Vector3.ClampMagnitude(PlayerInput, 1); //limita su magnitud a 1 para evitar una aceleracion en diagonales

         PlayerAmimatorController.SetFloat("PlayerVelocidadCaminar", PlayerInput.magnitude * Velocidad);

         DireccionCamara(); //Llamado a función DireccionCamara()

         DireccionPlayer = PlayerInput.x * CamDerecha + PlayerInput.z * CamDelante; //Almacena el vector de movimiento corregido con respecto a la camara

         DireccionPlayer = DireccionPlayer * Velocidad; //Y multiplica su valor por la velocidad del jugador

         Player.transform.LookAt(Player.transform.position + DireccionPlayer); //Hace que el personaje mire en la direccion en la que se está moviendo

         CrearGravedad(); //Llamado a función CrearGravedad()

         DeslizarCaida(); // Llamado a función DeslizarCaida()

         PlayerSkills(); // Llamado a función PlayerSkills()

         Player.Move(DireccionPlayer * Time.deltaTime);

        if (Player.isGrounded) PuedoSaltar = true; 
     }

     //Funcion para obetener la direccion de la camara
     private void DireccionCamara()
     {
         CamDelante = Cam.transform.forward;
         CamDerecha = Cam.transform.right;

         CamDelante.y = 0;
         CamDerecha.y = 0;

         CamDelante = CamDelante.normalized;
         CamDerecha = CamDerecha.normalized;
     }

     //Funcion para la gravedad
     private void CrearGravedad()
     {
         if (Player.isGrounded)
         {
             VelocidadCaida = -Gravedad * Time.deltaTime;
             DireccionPlayer.y = VelocidadCaida;
         }
         else
         {
             VelocidadCaida -= Gravedad * Time.deltaTime;
             DireccionPlayer.y = VelocidadCaida;

            if (VelocidadCaida < -5) PuedoSaltar = false;

            PlayerAmimatorController.SetFloat("PlayerVelocidadVertical", Player.velocity.y);
         }

         PlayerAmimatorController.SetBool("TocandoSuelo", Player.isGrounded);
     }

     //Funcion para las habilidades del player
     private void PlayerSkills()
     {
         if (PuedoSaltar && Input.GetButtonDown("Jump"))
         {
             VelocidadCaida = FuerzaSalto;
             DireccionPlayer.y = VelocidadCaida;

             PuedoSaltar = false;

             PlayerAmimatorController.SetTrigger("PlayerSalto");
         }
     }

     //Funcion para las pendientes
     private void DeslizarCaida()
     {
         EstaEnPendiente = Vector3.Angle(Vector3.up, HitNormal) >= Player.slopeLimit;

         if (EstaEnPendiente == true)
         {
             DireccionPlayer.x += ((1f - HitNormal.y) * HitNormal.x) * VelocidadPendiente;
             DireccionPlayer.z += ((1f - HitNormal.y) * HitNormal.z) * VelocidadPendiente;

             DireccionPlayer.y += FuerzaPendiente;
         }
     }

     private void OnControllerColliderHit(ControllerColliderHit hit)
     {
         HitNormal = hit.normal;
     }

     private void OnAnimatorMove()
     {

     }
}
