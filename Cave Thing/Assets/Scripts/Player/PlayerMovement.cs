using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float airSpeed;
    public float maxAirSpeed;
    public float jumpForce;
    public float horizLedgeJumpForce;
    public float vertLedgeJumpoForce;
    public float fallMultiplier;

    public Rigidbody2D rb;
    //private PlayerState currentState;

    private bool canMoveHoriz;
    private float horizInput;
    private float vertInput;
    public float accelerationModifier;

    private float jumpTimer;
    public float jumpRefreshTime;
    bool jumpHeld;

    private bool grounded;
    private bool touchingLeftWall;
    private bool touchingRightWall;
    public CheckLeftHand leftHandScript;
    bool grabbingLeft;
    bool grabbingRight;

    public bool facingLeft;
    public GameObject Sensors;

    // Start is called before the first frame update
    void Start()
    {
        canMoveHoriz = true;
        touchingLeftWall = false;
        touchingRightWall = false;
        horizInput = 0;
        jumpTimer = 0;
        jumpHeld = false;
        grabbingLeft = false;
        grabbingRight = false;
        facingLeft = true;
        //currentState = PlayerState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        vertInput = Input.GetAxis("Vertical");
        horizInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
            jumpTimer = 0;

        jumpTimer += Time.deltaTime;
        jumpHeld = Input.GetButton("Jump");

        updateFacing();
    }

    private void updateFacing()
    {
        if (rb.velocity.x > 0.1)
        {
            facingLeft = false;
            Sensors.transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else if (rb.velocity.x < -0.1)
        {
            facingLeft = true;
            Sensors.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

       
    }

    private void FixedUpdate()
    {
        moveLeftRight();
        jump();
    }

    private void jump()
    {
        bool jumpPressed = false;

        if (jumpTimer < jumpRefreshTime)
            jumpPressed = true;

        jumpTimer = 999;

        if (jumpPressed)
        {
            if (!grabbingLeft && !grabbingRight)
            {
                if (grounded)
                {

                    rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, jumpForce));
                }
            }
            else
            {
                if (vertInput < 0)
                {
                    if (grabbingLeft)
                    {
                        leftHandScript.setIsTrigger(true);
                        leftHandScript.setHandTouchingWall(true);
                    }
                }
                else
                {
                    rb.velocity = new Vector2(horizInput * horizLedgeJumpForce, Mathf.Max(rb.velocity.y, vertLedgeJumpoForce));
                }
                //hangingFromLedge = false;
            }
        }

        if (rb.velocity.y < -0 || !jumpHeld)
            rb.velocity += new Vector2(0, 0 - fallMultiplier);
    }

    private void moveLeftRight()
    {
        float targetSpeed = horizInput * speed;
        float acceleration = accelerationModifier * targetSpeed;

        if (canMoveHoriz)
        {
            rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
        }
    }

    private enum PlayerState
    {
        IDLE,
        RUNNING,
        AIRBORNE,
        CLIMBING
    }

    //GETTERS AND SETTERS

    public void setGrounded(bool grounded)
    {
        this.grounded = grounded;
    }

    public void setTouchingLeftWall(bool touching)
    {
        touchingLeftWall = touching;
    }
  
    public bool getTouchingLeftWall()
    {
        return touchingLeftWall;
    }

    public void setCanMoveHoriz(bool canMoveHoriz)
    {
        this.canMoveHoriz = canMoveHoriz;
    }

    public void setGrabbingLeft(bool grabbingLeft)
    {
        this.grabbingLeft = grabbingLeft;
        canMoveHoriz = !grabbingLeft;
    }
}
