using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Cámara")]
    public Transform playerCamera;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    [Header("Chequeo de suelo")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Posicion de objetos")]
    public Transform holdPoint;
    private GameObject heldObject;
  

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private RaycastHit lastHitInfo;
    private bool hitSomething = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // --- ROTACIÓN DE CÁMARA ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // --- CHEQUEO DE SUELO ---
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // --- MOVIMIENTO ---
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // --- SALTO ---
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // --- GRAVEDAD ---
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // --- INTERACCIÓN ---
        hitSomething = Physics.Raycast(playerCamera.position, playerCamera.forward, out lastHitInfo, 3f);
        if (hitSomething)
        {
            IInteractable interactable = lastHitInfo.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                Debug.Log(interactable.GetDescription());

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (heldObject == null)
                    {
                        interactable.Interact(); // por si quieres que haga algo más

                        // recoger objeto
                        heldObject = lastHitInfo.collider.gameObject;
                        heldObject.GetComponent<Rigidbody>().isKinematic = true;
                        heldObject.transform.SetParent(holdPoint);
                        heldObject.transform.localPosition = Vector3.zero;
                        heldObject.transform.localRotation = Quaternion.identity;
                    }
                    else
                    {
                        // soltar objeto
                        heldObject.transform.SetParent(null);
                        heldObject.GetComponent<Rigidbody>().isKinematic = false;
                        heldObject = null;
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = hitSomething ? Color.green : Color.red;
        Vector3 start = playerCamera.position;
        Vector3 end = hitSomething ? lastHitInfo.point : start + playerCamera.forward * 3f;
        Gizmos.DrawLine(start, end);

        if (hitSomething)
            Gizmos.DrawSphere(lastHitInfo.point, 0.1f);
    }
}

