using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed;
    public float sprintSpeed;
    private float moveSpeed;

    public float groundDrag;

    RaycastHit slopeHit;

    private Vector3 moveDirection;
    private Vector3 slopeMoveDirection;

    [Header("Jump")]
    public float jumpForce;
    public float airMultiplier;

    [Header("Crounching")]
    public float crouchSpeed;
    public float chrouchYScale;

    [Header("Source Inputs")]
    public XRNode movementSource;
    private Vector2 inputAxis;

    [Header("Ground Check")]
    public float playerHeight;
    public Transform groundCheck;
    public LayerMask isGround;
    bool isGrounded;

    public Camera camera;

    PlayerController controller;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<PlayerController>();

        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, isGround);

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        Inputs();
        DragControll();
        SpeedControll();
    }

    private void Inputs()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(movementSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

        if(controller.JumpPressed() && isGrounded)
        {
            Jump();
        }

        // Have to fix the crounch since also the hands are getting smaller
        if(controller.CrounchPressed())
        {
            transform.localScale = new Vector3(transform.localScale.x, chrouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (!controller.CrounchPressed())
        {
            transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
        }
    }

    private void MovePlayer()
    {
        Quaternion headYaw = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
        moveDirection = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Acceleration);
        }

        else if(isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * 10, ForceMode.Acceleration);
        }

        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10 * airMultiplier, ForceMode.Acceleration);
        }
    }

    private void DragControll()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }

        else
        {
            rb.drag = 3.5f;
        }
    }

    private void SpeedControll()
    {
        if(controller.SprintPressed() && isGrounded && !controller.CrounchPressed())
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, 10 * Time.deltaTime);
        }

        else if (!controller.SprintPressed() && isGrounded && !controller.CrounchPressed())
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, 10 * Time.deltaTime);
        }

        else if(controller.CrounchPressed() && !controller.SprintPressed())
        {
            moveSpeed = Mathf.Lerp(moveSpeed, crouchSpeed, 100 * Time.deltaTime);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        // To DO: Y position of the camera has to move
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        return false;
    }
}