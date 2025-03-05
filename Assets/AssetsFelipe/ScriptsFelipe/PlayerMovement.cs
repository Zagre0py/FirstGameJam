using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
        // Desactivar la rotaci�n del Rigidbody en los ejes X y Z para evitar rotaciones no deseadas
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
        
        if(moveX != 0 || moveZ !=0)
        {    
            animatorPlayer.SetBool("isWalking", true); 
        }
        else
        {
            animatorPlayer.SetBool("isWalking", false);
        }
    }

    // M�todo para manejar la rotaci�n de la c�mara con el rat�n
    private void HandleMouseLook()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;


        transform.Rotate(Vector3.up * mouseX);

        // Rotar la c�mara en el eje X (mirar arriba/abajo) con l�mites
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -lookUpDownLimit, lookUpDownLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void Jump()
    {
        // Verificar si el jugador est� tocando el suelo
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.3f, groundLayer);


        if (isGrounded && Input.GetButtonDown("Jump"))
        {

            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animatorPlayer.SetTrigger("isAttack");  // Usa SetTrigger en lugar de SetBool para animaciones de ataque
            AudioManager.instance.PlaySfx("AtaqueRaqueta");
        }
    }
}
