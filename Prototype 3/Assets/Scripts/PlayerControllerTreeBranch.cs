using UnityEngine;

public class PlayerControllerTreeBranch : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float mouseSensitivity = 2f;
    public Transform playerCamera;

    private Rigidbody rb;
    private float yawRotation = 0f;
    private float pitchRotation = 0f;

    public bool canMove = true;
    public bool canRotate = true; // Control whether player can rotate with the mouse
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerCamera.localRotation = Quaternion.Euler(pitchRotation, 0f, 0f);
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            HandleMovement();
            HandleJump();

            // Only rotate if canRotate is true
            if (canRotate)
            {
                HandleMouseRotation();
                HandleCameraPitch();
            }
        }
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = (transform.right * moveX + transform.forward * moveZ).normalized * moveSpeed;

        rb.MovePosition(rb.position + movement * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 jumpDirection = -Physics.gravity.normalized;
            rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        yawRotation += mouseX;

        Quaternion yawQuaternion = Quaternion.AngleAxis(yawRotation, -Physics.gravity.normalized);
        transform.rotation = yawQuaternion * Quaternion.FromToRotation(Vector3.up, -Physics.gravity.normalized);
    }

    private void HandleCameraPitch()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitchRotation -= mouseY;
        pitchRotation = Mathf.Clamp(pitchRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(pitchRotation, 0f, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }

    public void NotifyBranchDestruction()
    {
        // This function should be called by the branch before it is destroyed
        isGrounded = false;
    }
}
