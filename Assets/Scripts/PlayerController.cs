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

        //Inicializacion de variables
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

         PlayerAmimatorController.SetFloat("PlayerVelocidadCaminar", PlayerInput.magnitude * Velocidad); //Asigna un valor a la variable del Animator

        DireccionCamara(); //Llamado a función DireccionCamara()

         DireccionPlayer = PlayerInput.x * CamDerecha + PlayerInput.z * CamDelante; //Almacena el vector direccion corregido con respecto a la camara

         DireccionPlayer = DireccionPlayer * Velocidad; //Y multiplica su valor por la velocidad del jugador
         
         Player.transform.LookAt(Player.transform.position + DireccionPlayer); //Hace que el personaje mire en la direccion en la que se está moviendo

         CrearGravedad(); //Llamado a función CrearGravedad()

         DeslizarCaida(); // Llamado a función DeslizarCaida()

         PlayerSkills(); // Llamado a función PlayerSkills()

         Player.Move(DireccionPlayer * Time.deltaTime); //Hace mover al personaje

        if (Player.isGrounded) PuedoSaltar = true; 
     }

     //Funcion para obetener la direccion de la camara (hacia donde esta viendo)
     private void DireccionCamara()
     {
         CamDelante = Cam.transform.forward; //Guarda la direccion de la camara hacia delante
         CamDerecha = Cam.transform.right; //Guarda la direccion de la camara hacia la derecha

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
             VelocidadCaida = -Gravedad * Time.deltaTime; //Le aplica la gravedad para que se mantenga en contacto con el suelo
             DireccionPlayer.y = VelocidadCaida;
         }
         else
         {
             VelocidadCaida -= Gravedad * Time.deltaTime; //Crea una aceleracion de caida
             DireccionPlayer.y = VelocidadCaida;

            if (VelocidadCaida < -5) PuedoSaltar = false;
            
            PlayerAmimatorController.SetFloat("PlayerVelocidadVertical", Player.velocity.y); //Asigna un valor a la variable del Animator
        }

         PlayerAmimatorController.SetBool("TocandoSuelo", Player.isGrounded); //Asigna un valor a la variable del Animator
     }

     //Funcion para las habilidades del player
     private void PlayerSkills()
     {
         if (PuedoSaltar && Input.GetButtonDown("Jump"))
         {
             VelocidadCaida = FuerzaSalto;
             DireccionPlayer.y = VelocidadCaida;

             PuedoSaltar = false;

             PlayerAmimatorController.SetTrigger("PlayerSalto"); //Activa el trigger una vez que salto
         }
     }

     //Funcion para las pendientes
     private void DeslizarCaida()
     {
         EstaEnPendiente = Vector3.Angle(Vector3.up, HitNormal) >= Player.slopeLimit; //Calcula el angulo entre el vector UP y el HitNormal del objeto que golpeamos y lo compara con el slop del Character Controller

         if (EstaEnPendiente)
         {
            //Se le aplica la "fuerza" para hacer que caiga de la pendiente
             DireccionPlayer.x += ((1f - HitNormal.y) * HitNormal.x) * VelocidadPendiente; //Segun la inclinacion de la pendiente se aplica mas o menos velocidad
             DireccionPlayer.z += ((1f - HitNormal.y) * HitNormal.z) * VelocidadPendiente; //Segun la inclinacion de la pendiente se aplica mas o menos velocidad

            DireccionPlayer.y += FuerzaPendiente; //Se usa para evitar que vaya dando saltos
         }
     }

     private void OnControllerColliderHit(ControllerColliderHit hit) //Detecta cuando nuestro controller colisiona con otro objeto y lo almacena en hit
     {
         HitNormal = hit.normal;
     }

     private void OnAnimatorMove() //Se cre para que el Apply Root Motion no interfiera con el movimiento calculado que tenemos y las animaciones
     {

     }
}
