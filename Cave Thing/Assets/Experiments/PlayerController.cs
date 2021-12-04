using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //  player state
    private bool grounded;

    private InputAction movement;
    private Rigidbody2D rb;

    [SerializeField] private float speed;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpBuffer;
    private float jumpTimer;

    // Start is called before the first frame update
    
    private void OnEnable()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();
        movement = InputManager.inputActions.Player.Movement;
        movement.Enable();

        InputManager.inputActions.Player.Jump.performed += updateJumpTimer;
        InputManager.inputActions.Player.Jump.Enable();
    }
    private void OnDisable()
    {
        movement.Disable();
        InputManager.inputActions.Player.Jump.performed -= updateJumpTimer;
        InputManager.inputActions.Player.Jump.Disable();
    }

    private void updateJumpTimer(InputAction.CallbackContext obj)
    {
        jumpTimer = 0;
    }

    private void Update()
    {
        jumpTimer += Time.deltaTime;
        moveHorizontal();
        doJump();
    }

    private void moveHorizontal()
    {
        float targetSpeed = movement.ReadValue<float>() * speed;
        rb.velocity = new Vector2(targetSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void doJump()
    {
        // normal jump
        if(jumpTimer < jumpBuffer)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, jumpForce));
            jumpTimer = 999;
        }
    }
}
