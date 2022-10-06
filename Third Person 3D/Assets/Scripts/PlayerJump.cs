using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField]
    [Range(1f, 2000f)]
    private float movementForce = 3f;

    [SerializeField]
    [Range(1f, 2000f)]
    private float jumpForce = 40f;

    [SerializeField]
    [Range(1f, 200f)]
    private float maxSpeed = 5f;

    [SerializeField]
    [Range(1f, 200f)]
    private float delayNextJump = 1f;

    //---------------------- PROPIEDADES PUBLICAS ----------------------
    public bool CanJump { get => canJump; set => canJump = value; }
    public Rigidbody MyRigidbody { get => myRigidbody; set => myRigidbody = value; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private bool canJump = true;
    private bool inDelayJump = false;
    private float cameraAxisX = 0f;
    private Vector3 playerDirection;
    private Rigidbody myRigidbody;

    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        //PRIMERA FORMA DE ANIMAR CON MOVIMIENTO: ANIMAR ANTES SE MOVER
        //Elegimos una animacion en funci�n de la tecla que se empieza a presionar.
        bool forward = Input.GetKeyDown(KeyCode.W);
        bool back = Input.GetKeyDown(KeyCode.S);
        bool left = Input.GetKeyDown(KeyCode.A);
        bool right = Input.GetKeyDown(KeyCode.D);

        //Limpiamos la direcci�n de movimiento en cada frame.
        playerDirection = Vector3.zero;
        //Elegimos una direcci�n en funci�n de la tecla que se mantiene presionada.
        if (Input.GetKey(KeyCode.W)) playerDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) playerDirection += Vector3.back;
        if (Input.GetKey(KeyCode.D)) playerDirection += Vector3.right;
        if (Input.GetKey(KeyCode.A)) playerDirection += Vector3.left;

        //SOLUCION DE SALTO MANUAL
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            // playerDirection += Vector3.up * 50;
        }
    }
    /*
    M�todo FixedUpdate. Similar al Update, 
    pero se ejecuta una cantidad de veces fija por segunda.
    (50 x 0.02(valor configurable en Time) = 1 segundo) 
    (No depende de los FPS del ordenador)
    */
    private void FixedUpdate()
    {
        if (playerDirection != Vector3.zero && MyRigidbody.velocity.magnitude < MaxSpeed)
        {
            /*La fuerza a aplicar es constante (ForceMode.Force)
            Fuerzas constantes:Act�an de forma continuada sobre el rigidbody,
            aceler�ndolo continuamente mientras la fuerza es aplicada. 
            Un ejemplo de esta fuerza es la gravedad.
            */
            MyRigidbody.AddForce(transform.TransformDirection(playerDirection) * movementForce, ForceMode.Force);
            /*Para que la fueza se aplique tenieno presenta la rotacion local 
             necesito usar transform.TransformDirection para transfomar la direccion
             de movimiento a global.
            */
        }

        if (!canJump && !inDelayJump)
        {
            /*La fuerza a aplicar es instant�nea (ForceMode.Impulse)
            Fuerzas instant�neas: Act�an por un breve instante y, 
            por lo tanto, originan movimientos uniformes 
            acelerando la rigibody con un impulso.
            */
            MyRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
            inDelayJump = true;
            Invoke("DelayNextJump", delayNextJump);
        }
    }

    private void DelayNextJump()
    {
        inDelayJump = false;
        canJump = true;
    }


    public void RotatePlayer()
    {
        cameraAxisX += Input.GetAxis("Mouse X");
        Quaternion newRotation = Quaternion.Euler(0, cameraAxisX, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 2.5f * Time.deltaTime);
    }
}
