using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private InputAction movement;
    private Rigidbody2D rb;
    private BoxCollider2D collider;

    [SerializeField] private float speed;

    private float jumpTimer;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpBuffer;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private LayerMask checkGoundLayerMask;
    [SerializeField] private string[] groundTags;

    // state
    bool facingLeft;

    // Start is called before the first frame update

    private void OnEnable()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (animator == null)
            animator = GetComponent<Animator>();
        if (collider == null)
            collider = GetComponent<BoxCollider2D>();

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

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    
    private void Update()
    {
        jumpTimer += Time.deltaTime;
        moveHorizontal();
        doJump();
        updateAnimator();
        updateFacingLeft();
    }

    private void FixedUpdate()
    {
        verticalAcceleration();
    }

    private void updateAnimator()
    {
        animator.SetFloat("Horizontal Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("Vertical Speed", rb.velocity.y);
        animator.SetFloat("Vertical Absolute", Mathf.Abs(rb.velocity.y));

        animator.SetBool("Grounded", isGrounded());
        animator.SetBool("Facing Left", facingLeft);
    }

    private void moveHorizontal()
    {
        float targetSpeed = movement.ReadValue<float>() * speed;
        rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
    }

    private void updateJumpTimer (InputAction.CallbackContext obj)
    {
        jumpTimer = 0;
    }

    private void doJump()
    {
        // normal jump
        if (jumpTimer < jumpBuffer)
        {
            if (isGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, jumpForce));
                jumpTimer = 999;
            }
        }
    }

    private void verticalAcceleration()
    {
        if (rb.velocity.y < -0 || !jumpHeld())
            rb.velocity += new Vector2(0, 0 - fallMultiplier);
    }

    public bool isGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, extraHeight, checkGoundLayerMask);

        if (hit)
            return groundTags.Contains(hit.transform.tag);

        return false;
    }

    public bool jumpHeld()
    {
        return InputManager.inputActions.Player.Jump.IsPressed();
    }

    public void updateFacingLeft()
    {
        float move = movement.ReadValue<float>();

        if (move > 0)
            facingLeft = false;
        else if (move < 0)
            facingLeft = true;
    }
}
