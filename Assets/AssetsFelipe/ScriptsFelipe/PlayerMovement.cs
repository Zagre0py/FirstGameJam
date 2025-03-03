using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
   
    public float moveSpeed = 5f;        
    public float mouseSensitivity = 2f; 
    public float lookUpDownLimit = 80f; 
    public float jumpHeight = 2f;       
    public Transform groundCheck;       
    public LayerMask groundLayer;       


    private float xRotation = 0f;   
    private Rigidbody rb;           
    private bool isGrounded;        
    public Animator animatorPlayer;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        animatorPlayer.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;   // Bloquea el cursor en el centro
        Cursor.visible = false;
        Cursor.visible = false;
        // Desactivar la rotación del Rigidbody en los ejes X y Z para evitar rotaciones no deseadas
        rb.freezeRotation = true;
    }

    void Update()
    {

        Movement();//movement
        HandleMouseLook();//rotation
        Jump();//jumping
        Attack();//attack
    }


    private void Movement()
    {

        float moveX = Input.GetAxis("Horizontal") * moveSpeed; 
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;   

        // Calcular el movimiento en el espacio mundial
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    // Método para manejar la rotación de la cámara con el ratón
    private void HandleMouseLook()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;


        transform.Rotate(Vector3.up * mouseX);

        // Rotar la cámara en el eje X (mirar arriba/abajo) con límites
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -lookUpDownLimit, lookUpDownLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void Jump()
    {
        // Verificar si el jugador está tocando el suelo
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.3f, groundLayer);


        if (isGrounded && Input.GetButtonDown("Jump"))
        {

            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
    private void Attack()
    {
        if(Input.GetButtonDown("Fire1"))
        {

        animatorPlayer.SetBool("isAttacking", true);
        }
    }
}
