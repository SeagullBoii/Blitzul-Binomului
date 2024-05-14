using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float speed = 16f;
    [SerializeField] float jumpHeight = 3;
    [SerializeField] float maxCoyoteTime = 0.2f;
    [SerializeField] float maxTimeSinceJump = 0.2f;
    [SerializeField] float jumpCooldown = 0.3f;

    [Header("Camera")]
    [SerializeField] Transform camTilt;

    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundRadius = 0.4f;
    [SerializeField] LayerMask groundMask;

    float coyoteTimer = 0;
    float timeSinceJump = 0;
    float jumpCD = 0;

    bool grounded;
    bool justFell;
    Vector3 velocity;

    CharacterController controller;
    CameraLook cameraLook;
    const float GRAV_ACCEL = -19.81f; // Normal era -9.81f, dar am micsorat valoarea pentru a face movementul mai satisfacator

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        if (GetComponent<CameraLook>())
            cameraLook = GetComponent<CameraLook>();
    }

    private void Update()
    {

        grounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask); //O sfera la picioarele jucatorului care verifica daca e in aer sau nu.
        
        //Movementul in sine
        float xInput = Input.GetAxis("Horizontal"); // A si D
        float yInput = Input.GetAxis("Vertical"); //W si S

        Vector3 movementVector = transform.right * xInput + transform.forward * yInput;

        controller.Move(movementVector * speed * Time.deltaTime); //Utilizam movementul prebuilt de character controller

        if (cameraLook != null)
            cameraLook.TiltCamera(0, 0, 2 * -xInput, 0.5f); //Inclinam camera spre dreapta sau stanga
        

        //Saritul
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded || coyoteTimer > 0) 
                Jump();
            else
                timeSinceJump = maxTimeSinceJump;
        }

        if (grounded && timeSinceJump > 0) Jump();

        //Aplicarea fortei gravitationale
        Gravity();

        //Coyote Time si Jump Cooldown
        Timers();
    }

    private void Gravity() {
        if (grounded && velocity.y < 0) velocity.y = Mathf.Lerp(velocity.y, -2f, 10f * Time.deltaTime);
        velocity.y += GRAV_ACCEL * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    /// <summary>
    /// Variabila "timeSinceJump" reprezinta un timer de la ultima data cand jucatorul apasa space si "coyoteTimer" este un timer de cand jucatorul a cazut. 
    /// Deoarece jucatorul uneori nu stie exact cand cade, creem un timer de cand apasa space. Daca timerul nu trece de 0.2 secunde, jucatorul sare in momentul in care atinge pamantul.
    /// Pentru a face movementul mai satisfacator, ii permitem jucatorului sa sara, chiar daca nu este pe pamant, in cazul de la coyoteTimer, pentru ca exista un delay de input, abia cazand.
    /// Pentru ca exista un mic delay de cand este confirmat ca jucatorul a sarit, "jumpCD" asigura ca jucatorul nu poate sari de doua ori in acelasi timp.
    /// </summary>
    private void Jump() {
        timeSinceJump = maxTimeSinceJump;
        if (timeSinceJump > 0 && jumpCD <= 0)
            if (grounded || coyoteTimer > 0)
            {
                velocity.y = Mathf.Sqrt(-2f * jumpHeight * GRAV_ACCEL);
                jumpCD = jumpCooldown;
            }
    }

    /// <summary>
    /// Reseteaza timerele mentionate inainte de functia precedenta.
    /// "justFell" asigura ca variabila "coyoteTimer" sa nu se reseteze de mai multe ori in aer.
    /// Scaderea cu Time.deltaTime permite formarea un timer. De exemplu daca scad in fiecare frame din o variabila x = 1 Time.deltaTime, timerul o sa dureze exact o secunda
    /// </summary>
    private void Timers() {
        if (grounded) 
            justFell = true;
        else if (justFell)
        {
            coyoteTimer = maxCoyoteTime;
            justFell = false;
        }
        else if (coyoteTimer > 0) 
            coyoteTimer -= Time.deltaTime;

        if (timeSinceJump > 0) timeSinceJump -= Time.deltaTime;
        if (jumpCD > 0) jumpCD -= Time.deltaTime;
    }
}
