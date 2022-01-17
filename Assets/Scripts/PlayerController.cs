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

     public float FuerzaSalto;

     public float Velocidad;
     private Vector3 DireccionPlayer;

     public float Gravedad;
     private float VelocidadCaida;

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

         //Cursor.visible = false;

         EstaEnPendiente = false;
         VelocidadPendiente = 5f;
         FuerzaPendiente = -15f;

         // Gravedad = 9.81f;

         //FuerzaSalto = 10f;

         //Velocidad = 5;
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

             PlayerAmimatorController.SetFloat("PlayerVelocidadVertical", Player.velocity.y);
         }

         PlayerAmimatorController.SetBool("TocandoSuelo", Player.isGrounded);
     }

     //Funcion para las habilidades del player
     private void PlayerSkills()
     {
         if (Player.isGrounded && Input.GetButtonDown("Jump"))
         {
             VelocidadCaida = FuerzaSalto;
             DireccionPlayer.y = VelocidadCaida;

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



    /// <summary>
    /// //////////////////////////////////////////////////////////////////
    /// </summary>

    /*public float HMove;
    public float VMove;

    public CharacterController Player;

    private void Start()
    {
        Player = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HMove = Input.GetAxis("Horizontal");
        VMove = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Player.Move(new Vector3(HMove, 0, VMove));
    }*/

    ////////////////////////////////////////////////////
    ///
    /*
    public float forwardInput;
    public float rightInput;

    private Vector3 Velocity;

    // public float HMove;
    //public float VMove;

    public CameraController Camera;
    //public CharacterController Player;

    private void Start()
    {
        //Player = GetComponent<CharacterController>();
        transform.Translate(Velocity);
    }

    private void Update()
    {
        //HMove = Input.GetAxis("Horizontal");
        //VMove = Input.GetAxis("Vertical");

       // Player.Move(new Vector3(forwardInput, 0, rightInput));
        transform.Translate(Velocity);
    }

    public void AddMovementInput(float forward, float right)
    {
        forwardInput = forward;
        rightInput = right;

        Vector3 CamFwd = Camera.transform.forward;
        Vector3 CamRht = Camera.transform.right;

        Vector3 translation = forward * Camera.transform.forward;
        translation += right * Camera.transform.right;


        if (translation.magnitude > 0)
        {
            Velocity = translation;
        }
        else
        {
            Velocity = Vector3.zero;
        }
        
    }

    */
    
}
