using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed;
    public float sprintSpeed;
    private float moveSpeed;

    public float groundDrag;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Crounching")]
    public float crouchSpeed;
    public float chrouchYScale;

    [Header("Source Inputs")]
    public XRNode movementSource;
    private Vector2 inputAxis;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask isGround;
    bool grounded;

    public MovementState state;

    PlayerController controller;
    XROrigin rig;
    Rigidbody rb;

    public enum MovementState
    {
        walking,
        sprinting,
        crounching,
        air
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rig = GetComponentInChildren<XROrigin>();
        controller = GetComponent<PlayerController>();

        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, 4 * 0.5f + 0.2f, isGround);

        if (grounded)
            rb.drag = groundDrag;
        else if(!grounded)
            rb.drag = 0;

        Inputs();
        StateHandler();
        SpeedControll();
    }

    private void Inputs()
    {
        // Movement Axis
        InputDevice device = InputDevices.GetDeviceAtXRNode(movementSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

        // Jump
        if(controller.JumpPressed() && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // Start Crounch
        if(controller.CrounchPressed())
        {
            transform.localScale = new Vector3(transform.localScale.x, chrouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // Stop Crounch
        if (!controller.CrounchPressed())
        {
            transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
        }
    }
    private void StateHandler()
    {
        // State - Sprinting
        if(grounded && controller.SprintPressed() && !controller.CrounchPressed())
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // State - Walking
        else if (grounded && !controller.SprintPressed() && !controller.CrounchPressed())
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // State - Crounching
        else if (controller.CrounchPressed())
        {
            state = MovementState.crounching;
            moveSpeed = crouchSpeed;
        }

        // State - Air
        else if (!grounded)
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // Move and Turn the player
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        Vector3 orientation = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        if (grounded)
            rb.AddForce(orientation.normalized * moveSpeed * 10, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(orientation.normalized * moveSpeed * 10 * airMultiplier, ForceMode.Force);
    }

    private void SpeedControll()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, limitedVel.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
